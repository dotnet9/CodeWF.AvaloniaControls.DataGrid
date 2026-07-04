using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
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

public static class TreeDataGridExtension
{
    private static readonly ConditionalWeakTable<TreeDataGrid, TreeDataGridSortingState> SortingRegistrations = new();

    /// <summary>
    /// 为 TreeDataGrid 添加三态排序：升序、降序、取消排序。
    /// </summary>
    public static void AddSorting<T>(this TreeDataGrid treeDataGrid, FlatTreeDataGridSource<T> itemSource)
        where T : class
    {
        if (SortingRegistrations.TryGetValue(treeDataGrid, out _))
        {
            return;
        }

        var state = new TreeDataGridSortingState();
        SortingRegistrations.Add(treeDataGrid, state);

        treeDataGrid.CanUserSortColumns = false;
        treeDataGrid.AddHandler(Button.ClickEvent, (_, e) =>
        {
            if (e.Source is not TreeDataGridColumnHeader header ||
                treeDataGrid.Source is not ITreeDataGridSource source ||
                source.Columns is not IList columns ||
                header.ColumnIndex < 0 ||
                header.ColumnIndex >= columns.Count ||
                columns[header.ColumnIndex] is not IColumn column)
            {
                return;
            }

            e.Handled = true;
            treeDataGrid.CanUserSortColumns = false;

            var nextDirection = state.GetNextDirection(column);
            if (nextDirection is null)
            {
                state.Clear();
                ClearSorting(treeDataGrid, source, columns);
                return;
            }

            if (source.SortBy(column, nextDirection.Value))
            {
                state.Set(column, nextDirection.Value);
            }
            else
            {
                state.Clear();
            }
        }, RoutingStrategies.Bubble, handledEventsToo: true);
    }

    private sealed class TreeDataGridSortingState
    {
        private IColumn? _column;
        private ListSortDirection? _direction;

        public ListSortDirection? GetNextDirection(IColumn column)
        {
            if (!ReferenceEquals(_column, column))
            {
                return ListSortDirection.Ascending;
            }

            return _direction switch
            {
                null => ListSortDirection.Ascending,
                ListSortDirection.Ascending => ListSortDirection.Descending,
                ListSortDirection.Descending => null,
                _ => ListSortDirection.Ascending
            };
        }

        public void Set(IColumn column, ListSortDirection direction)
        {
            _column = column;
            _direction = direction;
        }

        public void Clear()
        {
            _column = null;
            _direction = null;
        }
    }

    /// <summary>
    /// 为 TreeDataGrid 添加 Ctrl+A 全选，以及 Ctrl+Shift+A 取消全选。
    /// </summary>
    public static void AddSelectAll<T>(this TreeDataGrid treeDataGrid, FlatTreeDataGridSource<T> itemSource)
        where T : class
    {
        treeDataGrid.AddHandler(InputElement.KeyDownEvent, (_, e) =>
        {
            if (e.KeyModifiers == KeyModifiers.Control && e.Key == Key.A)
            {
                itemSource.RowSelection?.Clear();
                itemSource.RowSelection?.BeginBatchUpdate();

                for (var i = 0; i < itemSource.Rows.Count; i++)
                {
                    itemSource.RowSelection?.Select(new IndexPath(i));
                }

                itemSource.RowSelection?.EndBatchUpdate();
                e.Handled = true;
            }
            else if (e.KeyModifiers == (KeyModifiers.Control | KeyModifiers.Shift) && e.Key == Key.A)
            {
                itemSource.RowSelection?.Clear();
                e.Handled = true;
            }
        }, RoutingStrategies.Tunnel);
    }

    /// <summary>
    /// 为 TreeDataGrid 启用智能 ToolTip，只在内容显示不全时显示。
    /// </summary>
    public static void EnableSmartTooltips(this TreeDataGrid treeDataGrid)
    {
        treeDataGrid.RowPrepared += (_, e) =>
        {
            var row = e.Row;
            if (row is null)
            {
                return;
            }

            DispatcherTimer.RunOnce(() => ProcessTreeDataGridRow(row), TimeSpan.FromMilliseconds(1000));
        };
    }

    /// <summary>
    /// 为 TreeDataGrid 启用智能 ToolTip，可指定要处理的列索引。
    /// </summary>
    public static void EnableSmartTooltips(this TreeDataGrid treeDataGrid, params int[] targetColumnIndexes)
    {
        treeDataGrid.RowPrepared += (_, e) =>
        {
            var row = e.Row;
            if (row is null)
            {
                return;
            }

            DispatcherTimer.RunOnce(() => ProcessTreeDataGridRow(row, targetColumnIndexes), TimeSpan.FromMilliseconds(1000));
        };
    }

    [UnconditionalSuppressMessage("Trimming", "IL2075", Justification = "Legacy TreeDataGrid 11.1.1 has no public API to clear sorting.")]
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "Legacy TreeDataGrid 11.1.1 has no public API to clear sorting.")]
    private static void ClearSorting(TreeDataGrid treeDataGrid, ITreeDataGridSource source, IList columns)
    {
        foreach (var item in columns)
        {
            if (item is IColumn column)
            {
                column.SortDirection = null;
            }
        }

        ClearPrivateField(treeDataGrid, "_userSortColumn");
        SetPrivateField(treeDataGrid, "_userSortDirection", ListSortDirection.Ascending);
        ClearPrivateField(source, "_comparer");
        ClearPrivateField(source, "_comparison");

        var rows = source.Rows;
        ClearPrivateField(rows, "_sortedIndexes");
        ClearPrivateField(rows, "_comparer");
        ClearPrivateField(rows, "_comparison");

        treeDataGrid.Source = null;
        treeDataGrid.Source = source;
    }

    [UnconditionalSuppressMessage("Trimming", "IL2075", Justification = "Legacy TreeDataGrid 11.1.1 has no public API to clear sorting.")]
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "Legacy TreeDataGrid 11.1.1 has no public API to clear sorting.")]
    private static void ClearPrivateField(object target, string fieldName)
    {
        SetPrivateField(target, fieldName, null);
    }

    [UnconditionalSuppressMessage("Trimming", "IL2075", Justification = "Legacy TreeDataGrid 11.1.1 has no public API to clear sorting.")]
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "Legacy TreeDataGrid 11.1.1 has no public API to clear sorting.")]
    private static void SetPrivateField(object target, string fieldName, object? value)
    {
        var flags = BindingFlags.Instance | BindingFlags.NonPublic;
        target.GetType().GetField(fieldName, flags)?.SetValue(target, value);
    }

    private static void ProcessTreeDataGridRow(TreeDataGridRow? row, int[]? targetColumnIndexes = default)
    {
        if (row is null)
        {
            return;
        }

        var cells = new List<(Visual Visual, int ColumnIndex)>();
        FindTreeDataGridCells(row, cells);

        if (targetColumnIndexes?.Any() != true)
        {
            if (cells.Count == 0)
            {
                ProcessCell(row);
                return;
            }

            cells.ForEach(cell => ProcessCell(cell.Visual));
            return;
        }

        var targetColumns = new HashSet<int>(targetColumnIndexes.Where(x => x >= 0));
        foreach (var cell in cells)
        {
            if (targetColumns.Contains(cell.ColumnIndex))
            {
                ProcessCell(cell.Visual);
            }
        }
    }

    private static void ProcessCell(Visual? visual)
    {
        if (visual is null)
        {
            return;
        }

        var textBlocks = new List<TextBlock>();
        FindVisualChildren(visual, textBlocks);

        textBlocks.ForEach(SetupSmartTooltip);
    }

    private static void FindTreeDataGridCells(Visual? visual, List<(Visual Visual, int ColumnIndex)> cells)
    {
        if (visual is null)
        {
            return;
        }

        foreach (var child in visual.GetVisualChildren())
        {
            if (child is Visual visualChild)
            {
                var columnIndex = TryGetTreeDataGridCellColumnIndex(visualChild);
                if (columnIndex is not null)
                {
                    cells.Add((visualChild, columnIndex.Value));
                }

                FindTreeDataGridCells(visualChild, cells);
            }
        }
    }

    [UnconditionalSuppressMessage("Trimming", "IL2075", Justification = "Legacy TreeDataGrid 11.1.1 exposes cell column indexes through an internal visual type.")]
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "Legacy TreeDataGrid 11.1.1 exposes cell column indexes through an internal visual type.")]
    private static int? TryGetTreeDataGridCellColumnIndex(Visual visual)
    {
        var property = visual.GetType().GetProperty("ColumnIndex", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (property?.PropertyType != typeof(int))
        {
            return null;
        }

        return property.GetValue(visual) is int columnIndex ? columnIndex : null;
    }

    private static void FindVisualChildren<T>(Visual? visual, List<T> array) where T : Visual
    {
        if (visual is null)
        {
            return;
        }

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
