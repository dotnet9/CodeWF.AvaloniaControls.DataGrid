using CodeWF.AvaloniaControls;
using CodeWF.AvaloniaControls.DataGridLegacyDemo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace CodeWF.AvaloniaControls.DataGridLegacyDemo.ViewModels;

internal sealed class LegacyProcessRecordSortComparer<TValue> : IDataGridSortDirectionAwareComparer
{
    private readonly Func<LegacyProcessRecord, TValue?> _selector;

    public Func<LegacyProcessRecord, bool>? ShouldPin { get; set; }

    public ListSortDirection SortDirection { get; set; } = ListSortDirection.Ascending;

    public LegacyProcessRecordSortComparer(Func<LegacyProcessRecord, TValue?> selector)
    {
        _selector = selector;
    }

    public int Compare(object? x, object? y)
    {
        if (ReferenceEquals(x, y))
        {
            return 0;
        }

        if (x is not LegacyProcessRecord xRecord)
        {
            return y is LegacyProcessRecord ? -1 : 0;
        }

        if (y is not LegacyProcessRecord yRecord)
        {
            return 1;
        }

        var pinComparison = GetPinRank(xRecord).CompareTo(GetPinRank(yRecord));
        if (pinComparison != 0)
        {
            return SortDirection == ListSortDirection.Descending ? -pinComparison : pinComparison;
        }

        return CompareValues(_selector(xRecord), _selector(yRecord));
    }

    private int GetPinRank(LegacyProcessRecord record)
    {
        return ShouldPin?.Invoke(record) == true ? 0 : 1;
    }

    private static int CompareValues(TValue? x, TValue? y)
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
}
