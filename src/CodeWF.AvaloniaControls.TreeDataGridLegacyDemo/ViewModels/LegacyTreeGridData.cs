using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            options: CreatePinnedSortOptions(getter.Compile(), shouldPin));
    }

    private static TextColumnOptions<LegacyTreeRecord>? CreatePinnedSortOptions<TValue>(
        Func<LegacyTreeRecord, TValue?> selector,
        Func<LegacyTreeRecord, bool>? shouldPin)
    {
        if (shouldPin is null)
        {
            return null;
        }

        return new TextColumnOptions<LegacyTreeRecord>
        {
            CompareAscending = (x, y) => ComparePinned(x, y, selector, shouldPin, descending: false),
            CompareDescending = (x, y) => ComparePinned(x, y, selector, shouldPin, descending: true),
        };
    }

    private static int ComparePinned<TValue>(
        LegacyTreeRecord? x,
        LegacyTreeRecord? y,
        Func<LegacyTreeRecord, TValue?> selector,
        Func<LegacyTreeRecord, bool> shouldPin,
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

        var valueComparison = Comparer<TValue>.Default.Compare(selector(x), selector(y));
        return descending ? -valueComparison : valueComparison;
    }

    private static int GetPinRank(LegacyTreeRecord record, Func<LegacyTreeRecord, bool> shouldPin)
    {
        return shouldPin(record) ? 0 : 1;
    }

    public static List<LegacyTreeRecord> CreateCommonRecords(int count)
    {
        var items = new List<LegacyTreeRecord>(count);

        for (var i = 1; i <= count; i++)
        {
            var lineNo = i % 5 + 1;
            items.Add(new LegacyTreeRecord
            {
                Id = i,
                Name = $"普通节点 {i}",
                Line = $"南区 {lineNo} 号线",
                Host = $"10.40.{lineNo}.{i % 220 + 10}",
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

        for (var i = 1; i <= count; i++)
        {
            var lineNo = (i + seed) % 6 + 1;
            items.Add(new LegacyTreeRecord
            {
                Id = i,
                Name = $"工艺节点 {i}",
                Line = $"东区 {lineNo} 号线",
                Host = $"10.30.{seed % 18}.{i % 220 + 10}",
                Mode = i % 4 == 0 ? "巡检" : "同步",
                Owner = i % 5 == 0 ? "夜班值守" : "调度中心",
                Status = i % 7 == 0 ? "待处理" : "运行中"
            });
        }

        return items;
    }
}
