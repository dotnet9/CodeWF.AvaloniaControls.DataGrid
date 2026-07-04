using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Reactive;
using Avalonia.Threading;
using Avalonia.VisualTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CodeWF.AvaloniaControls;

public interface IDataGridSortDirectionAwareComparer : IComparer
{
    ListSortDirection SortDirection { get; set; }
}

public static class DataGridExtension
{
    private static readonly ConditionalWeakTable<DataGrid, DataGridSortingState> SortingRegistrations = new();
    private static readonly MethodInfo? GetSortPropertyNameMethod =
        typeof(DataGridColumn).GetMethod("GetSortPropertyName", BindingFlags.Instance | BindingFlags.NonPublic);

    /// <summary>
    /// 为 DataGrid 添加三态排序：升序、降序、取消排序。
    /// </summary>
    public static void AddSorting(this DataGrid dataGrid)
    {
        if (SortingRegistrations.TryGetValue(dataGrid, out _))
        {
            return;
        }

        var state = new DataGridSortingState();
        SortingRegistrations.Add(dataGrid, state);

        dataGrid.Sorting += (s, e) =>
        {
            e.Handled = true;

            if (s is not DataGrid grid)
            {
                return;
            }

            var view = GetOrCreateCollectionView(grid);
            if (view is null)
            {
                state.Clear();
                return;
            }

            var nextDirection = state.GetNextDirection(e.Column);
            view.SortDescriptions.Clear();

            if (nextDirection is not null)
            {
                var sortDescription = CreateSortDescription(e.Column, nextDirection.Value, view.Culture);
                if (sortDescription is null)
                {
                    state.Clear();
                }
                else
                {
                    view.SortDescriptions.Add(sortDescription);
                }
            }

            view.Refresh();
        };
    }

    private static DataGridCollectionView? GetOrCreateCollectionView(DataGrid dataGrid)
    {
        if (dataGrid.ItemsSource is DataGridCollectionView view)
        {
            return view;
        }

        if (dataGrid.ItemsSource is not IEnumerable source)
        {
            return null;
        }

        view = new DataGridCollectionView(source);
        dataGrid.ItemsSource = view;
        return view;
    }

    private static DataGridSortDescription? CreateSortDescription(
        DataGridColumn column,
        ListSortDirection direction,
        CultureInfo culture)
    {
        if (column.CustomSortComparer is { } comparer)
        {
            if (comparer is IDataGridSortDirectionAwareComparer directionAwareComparer)
            {
                directionAwareComparer.SortDirection = direction;
            }

            return DataGridSortDescription.FromComparer(comparer, direction);
        }

        var memberPath = GetSortPropertyName(column);
        return string.IsNullOrWhiteSpace(memberPath)
            ? null
            : DataGridSortDescription.FromPath(memberPath, direction, culture);
    }

    [UnconditionalSuppressMessage("Trimming", "IL2075", Justification = "Avalonia DataGrid derives the default sort path from an internal helper.")]
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "Avalonia DataGrid derives the default sort path from an internal helper.")]
    private static string? GetSortPropertyName(DataGridColumn column)
    {
        return GetSortPropertyNameMethod?.Invoke(column, null) as string ?? column.SortMemberPath;
    }

    private sealed class DataGridSortingState
    {
        private DataGridColumn? _column;
        private ListSortDirection? _direction;

        public ListSortDirection? GetNextDirection(DataGridColumn column)
        {
            if (!ReferenceEquals(_column, column))
            {
                _column = column;
                _direction = ListSortDirection.Ascending;
                return _direction;
            }

            _direction = _direction switch
            {
                null => ListSortDirection.Ascending,
                ListSortDirection.Ascending => ListSortDirection.Descending,
                ListSortDirection.Descending => null,
                _ => ListSortDirection.Ascending
            };

            if (_direction is null)
            {
                _column = null;
            }

            return _direction;
        }

        public void Clear()
        {
            _column = null;
            _direction = null;
        }
    }

    /// <summary>
    /// 判断是否双击了 DataGrid 的行。
    /// </summary>
    public static bool IsDoubleClickRow(TappedEventArgs? e)
    {
        if (e is null)
        {
            return true;
        }

        if (e.Source is not Control control)
        {
            return false;
        }

        var row = control.FindAncestorOfType<DataGridRow>();
        return row is not null;
    }

    /// <summary>
    /// 为 DataGrid 启用智能 ToolTip，只在内容显示不全时显示。
    /// </summary>
    public static void EnableSmartTooltips(this DataGrid dataGrid)
    {
        dataGrid.LoadingRow += (_, e) =>
            DispatcherTimer.RunOnce(() => ProcessDataGridRow(e.Row), TimeSpan.FromMilliseconds(1000));
    }

    /// <summary>
    /// 为 DataGrid 启用智能 ToolTip，可指定要处理的列索引。
    /// </summary>
    public static void EnableSmartTooltips(this DataGrid dataGrid, params int[] targetColumnIndexes)
    {
        dataGrid.LoadingRow += (_, e) =>
            DispatcherTimer.RunOnce(() => ProcessDataGridRow(e.Row, targetColumnIndexes), TimeSpan.FromMilliseconds(1000));
    }

    private static void ProcessDataGridRow(DataGridRow row, int[]? targetColumnIndexes = default)
    {
        var cells = new List<DataGridCell>();
        FindVisualChildren(row, cells);

        if (targetColumnIndexes?.Any() != true)
        {
            cells.ForEach(ProcessCell);
            return;
        }

        for (var i = 0; i < targetColumnIndexes.Length; i++)
        {
            var columnIndex = targetColumnIndexes[i];
            if (columnIndex < 0 || columnIndex >= cells.Count)
            {
                continue;
            }

            ProcessCell(cells[columnIndex]);
        }
    }

    private static void ProcessCell(DataGridCell cell)
    {
        var textBlocks = new List<TextBlock>();
        FindVisualChildren(cell, textBlocks);

        textBlocks.ForEach(SetupSmartTooltip);
    }

    private static void FindVisualChildren<T>(Visual visual, List<T> array) where T : Visual
    {
        foreach (var child in visual.GetVisualChildren())
        {
            if (child is T t)
            {
                array.Add(t);
            }

            if (child is Visual visualChild)
            {
                FindVisualChildren(visualChild, array);
            }
        }
    }

    private static void SetupSmartTooltip(TextBlock textBlock)
    {
        if (textBlock.Tag != null)
        {
            return;
        }

        textBlock.Tag = true;
        textBlock.TextTrimming = TextTrimming.CharacterEllipsis;

        UpdateToolTip(textBlock);
        textBlock.GetObservable(TextBlock.TextProperty)
            .Subscribe(new AnonymousObserver<string?>(_ => UpdateToolTip(textBlock)));
        textBlock.GetObservable(Visual.BoundsProperty)
            .Subscribe(new AnonymousObserver<Rect>(_ => UpdateToolTip(textBlock)));
    }

    private static void UpdateToolTip(TextBlock textBlock)
    {
        try
        {
            if (string.IsNullOrEmpty(textBlock.Text) || textBlock.Bounds.Width <= 0)
            {
                ToolTip.SetTip(textBlock, null);
                return;
            }

            var formattedText = new FormattedText(
                textBlock.Text!,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight),
                textBlock.FontSize,
                Brushes.Black);

            ToolTip.SetTip(textBlock, formattedText.Width > textBlock.Bounds.Width ? textBlock.Text : null);
        }
        catch
        {
        }
    }
}
