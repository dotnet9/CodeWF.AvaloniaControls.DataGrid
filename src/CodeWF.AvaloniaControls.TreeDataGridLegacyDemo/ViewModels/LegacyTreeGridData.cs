using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.ViewModels;

internal static class LegacyTreeGridData
{
    public static FlatTreeDataGridSource<LegacyTreeRecord> CreateSource(
        ObservableCollection<LegacyTreeRecord> records,
        Func<LegacyTreeRecord, bool>? shouldPin = null)
    {
        return new FlatTreeDataGridSource<LegacyTreeRecord>(records)
        {
            Columns =
            {
                CreateTextColumn("编号", x => x.Id, shouldPin),
                CreateTextColumn("任务名称", x => x.Name, shouldPin),
                CreateTextColumn("产线", x => x.Line, shouldPin),
                CreateTextColumn("主机", x => x.Host, shouldPin),
                CreateTextColumn("模式", x => x.Mode, shouldPin),
                CreateTextColumn("责任人", x => x.Owner, shouldPin),
                CreateTextColumn("状态", x => x.Status, shouldPin),
            }
        };
    }

    private static TextColumn<LegacyTreeRecord, TValue> CreateTextColumn<TValue>(
        string header,
        Expression<Func<LegacyTreeRecord, TValue?>> getter,
        Func<LegacyTreeRecord, bool>? shouldPin)
    {
        return new TextColumn<LegacyTreeRecord, TValue>(
            header,
            getter,
            options: CreateSortOptions(getter.Compile(), shouldPin));
    }

    private static TextColumnOptions<LegacyTreeRecord> CreateSortOptions<TValue>(
        Func<LegacyTreeRecord, TValue?> selector,
        Func<LegacyTreeRecord, bool>? shouldPin)
    {
        return new TextColumnOptions<LegacyTreeRecord>
        {
            CompareAscending = (x, y) => CompareRecords(x, y, selector, shouldPin, descending: false),
            CompareDescending = (x, y) => CompareRecords(x, y, selector, shouldPin, descending: true),
        };
    }

    private static int CompareRecords<TValue>(
        LegacyTreeRecord? x,
        LegacyTreeRecord? y,
        Func<LegacyTreeRecord, TValue?> selector,
        Func<LegacyTreeRecord, bool>? shouldPin,
        bool descending)
    {
        if (ReferenceEquals(x, y))
        {
            return 0;
        }

        if (x is null)
        {
            return 1;
        }

        if (y is null)
        {
            return -1;
        }

        var pinComparison = GetPinRank(x, shouldPin).CompareTo(GetPinRank(y, shouldPin));
        if (pinComparison != 0)
        {
            return pinComparison;
        }

        var valueComparison = CompareValues(selector(x), selector(y));
        return descending ? -valueComparison : valueComparison;
    }

    private static int GetPinRank(LegacyTreeRecord record, Func<LegacyTreeRecord, bool>? shouldPin)
    {
        return shouldPin?.Invoke(record) == true ? 0 : 1;
    }

    private static int CompareValues<TValue>(TValue? x, TValue? y)
    {
        if (x is string xText && y is string yText)
        {
            return NaturalStringComparer.Compare(xText, yText);
        }

        return Comparer<TValue>.Default.Compare(x, y);
    }

    private static class NaturalStringComparer
    {
        private static readonly CompareInfo CompareInfo = CultureInfo.CurrentCulture.CompareInfo;

        public static int Compare(string? x, string? y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (x is null)
            {
                return -1;
            }

            if (y is null)
            {
                return 1;
            }

            var xIndex = 0;
            var yIndex = 0;
            while (xIndex < x.Length && yIndex < y.Length)
            {
                var xIsDigit = char.IsDigit(x[xIndex]);
                var yIsDigit = char.IsDigit(y[yIndex]);
                if (xIsDigit && yIsDigit)
                {
                    var comparison = CompareNumberRun(x, ref xIndex, y, ref yIndex);
                    if (comparison != 0)
                    {
                        return comparison;
                    }

                    continue;
                }

                var textComparison = CompareTextRun(x, ref xIndex, y, ref yIndex);
                if (textComparison != 0)
                {
                    return textComparison;
                }
            }

            return x.Length.CompareTo(y.Length);
        }

        private static int CompareTextRun(string x, ref int xIndex, string y, ref int yIndex)
        {
            var xStart = xIndex;
            var yStart = yIndex;

            while (xIndex < x.Length && !char.IsDigit(x[xIndex]))
            {
                xIndex++;
            }

            while (yIndex < y.Length && !char.IsDigit(y[yIndex]))
            {
                yIndex++;
            }

            if (xStart == xIndex && xIndex < x.Length)
            {
                xIndex++;
            }

            if (yStart == yIndex && yIndex < y.Length)
            {
                yIndex++;
            }

            return CompareInfo.Compare(
                x[xStart..xIndex],
                y[yStart..yIndex],
                CompareOptions.StringSort);
        }

        private static int CompareNumberRun(string x, ref int xIndex, string y, ref int yIndex)
        {
            var xStart = xIndex;
            var yStart = yIndex;

            while (xIndex < x.Length && char.IsDigit(x[xIndex]))
            {
                xIndex++;
            }

            while (yIndex < y.Length && char.IsDigit(y[yIndex]))
            {
                yIndex++;
            }

            var xSignificant = SkipLeadingZeros(x, xStart, xIndex);
            var ySignificant = SkipLeadingZeros(y, yStart, yIndex);
            var xSignificantLength = xIndex - xSignificant;
            var ySignificantLength = yIndex - ySignificant;

            if (xSignificantLength != ySignificantLength)
            {
                return xSignificantLength.CompareTo(ySignificantLength);
            }

            for (var i = 0; i < xSignificantLength; i++)
            {
                var comparison = x[xSignificant + i].CompareTo(y[ySignificant + i]);
                if (comparison != 0)
                {
                    return comparison;
                }
            }

            return (xIndex - xStart).CompareTo(yIndex - yStart);
        }

        private static int SkipLeadingZeros(string value, int start, int end)
        {
            while (start < end - 1 && value[start] == '0')
            {
                start++;
            }

            return start;
        }
    }

    public static List<LegacyTreeRecord> CreateCommonRecords(int count)
    {
        var items = new List<LegacyTreeRecord>(count);
        var nameNumbers = CreateShuffledNumbers(count, 20260704);
        var random = new Random(20260705);

        for (var i = 1; i <= count; i++)
        {
            var groupNo = i % 5 + 1;
            var lineNo = random.Next(1, 999);
            items.Add(new LegacyTreeRecord
            {
                Id = i,
                Name = $"普通节点 {nameNumbers[i - 1]}",
                Line = $"南区产线 {lineNo}",
                Host = $"10.40.{groupNo}.{i % 220 + 10}",
                Mode = i % 3 == 0 ? "手动" : "自动",
                Owner = i % 4 == 0 ? "现场班组" : "调度中心",
                Status = i % 6 == 0 ? "维护中" : "运行中"
            });
        }

        return items;
    }

    public static List<LegacyTreeRecord> CreateStressRecords(int count, int seed)
    {
        var items = new List<LegacyTreeRecord>(count);
        var nameNumbers = CreateShuffledNumbers(count, seed + 4096);
        var random = new Random(seed + 8192);

        for (var i = 1; i <= count; i++)
        {
            var groupNo = (i + seed) % 6 + 1;
            var lineNo = random.Next(1, 2000);
            items.Add(new LegacyTreeRecord
            {
                Id = i,
                Name = $"工艺节点 {nameNumbers[i - 1]}",
                Line = $"东区产线 {lineNo}",
                Host = $"10.30.{groupNo}.{i % 220 + 10}",
                Mode = i % 4 == 0 ? "巡检" : "同步",
                Owner = i % 5 == 0 ? "夜班值守" : "调度中心",
                Status = i % 7 == 0 ? "待处理" : "运行中"
            });
        }

        return items;
    }

    private static List<int> CreateShuffledNumbers(int count, int seed)
    {
        var numbers = Enumerable.Range(1, count).ToList();
        var random = new Random(seed);

        for (var i = numbers.Count - 1; i > 0; i--)
        {
            var targetIndex = random.Next(i + 1);
            (numbers[i], numbers[targetIndex]) = (numbers[targetIndex], numbers[i]);
        }

        var firstValueIndex = numbers.IndexOf(1);
        if (firstValueIndex > 0)
        {
            (numbers[0], numbers[firstValueIndex]) = (numbers[firstValueIndex], numbers[0]);
        }

        return numbers;
    }
}
