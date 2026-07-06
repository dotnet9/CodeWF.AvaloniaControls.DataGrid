using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Collections;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using CodeWF.AvaloniaControls.DataGridDemo.Models;
using CodeWF.AvaloniaControls.DataGridDemo.ViewModels;
using System;
using System.Linq;
using AvaloniaDataGrid = Avalonia.Controls.DataGrid;

namespace CodeWF.AvaloniaControls.DataGridDemo.Views;

public partial class CommonDataGridView : UserControl
{
    private static readonly IBrush HighlightBrush = SolidColorBrush.Parse("#E5E7EB");
    private CommonDataGridViewModel? _viewModel;

    public CommonDataGridView()
    {
        InitializeComponent();

        ConfigureDataGrid(CommonDataGrid);
        CommonDataGrid.LoadingRow += CommonDataGrid_LoadingRow;
        DataContextChanged += (_, _) => ConfigureSource();
    }

    private void ConfigureSource()
    {
        if (DataContext is not CommonDataGridViewModel viewModel)
        {
            _viewModel = null;
            CommonDataGrid.ItemsSource = null;
            return;
        }

        if (ReferenceEquals(_viewModel, viewModel))
        {
            return;
        }

        _viewModel = viewModel;
        CommonDataGrid.ItemsSource = viewModel.Records;
        ConfigurePinnedSortComparers(viewModel);
        CommonDataGrid.EnableDefaults();
        RefreshVisibleRows();
    }

    private static void ConfigureDataGrid(AvaloniaDataGrid dataGrid)
    {
        EnsureColumns(dataGrid);
        dataGrid.RowHeight = 36;
        dataGrid.CanUserReorderColumns = false;
        dataGrid.CanUserResizeColumns = true;
        dataGrid.GridLinesVisibility = DataGridGridLinesVisibility.Horizontal;
    }

    private void ConfigurePinnedSortComparers(CommonDataGridViewModel viewModel)
    {
        foreach (var column in CommonDataGrid.Columns)
        {
            if (column.CustomSortComparer is LegacyProcessRecordSortComparer<int> intComparer)
            {
                intComparer.ShouldPin = viewModel.ShouldHighlight;
            }
            else if (column.CustomSortComparer is LegacyProcessRecordSortComparer<string> stringComparer)
            {
                stringComparer.ShouldPin = viewModel.ShouldHighlight;
            }
        }
    }

    private static void EnsureColumns(AvaloniaDataGrid dataGrid)
    {
        if (dataGrid.Columns.Count > 0)
        {
            return;
        }

        AddColumn(dataGrid, "编号", nameof(LegacyProcessRecord.Id), 90, x => x.Id);
        AddColumn(dataGrid, "任务名称", nameof(LegacyProcessRecord.Name), 180, x => x.Name);
        AddColumn(dataGrid, "产线", nameof(LegacyProcessRecord.Line), 120, x => x.Line);
        AddColumn(dataGrid, "主机", nameof(LegacyProcessRecord.Host), 180, x => x.Host);
        AddColumn(dataGrid, "程序路径", nameof(LegacyProcessRecord.ProgramPath), 260, x => x.ProgramPath);
        AddColumn(dataGrid, "工作路径", nameof(LegacyProcessRecord.WorkPath), 240, x => x.WorkPath);
        AddColumn(dataGrid, "启动参数", nameof(LegacyProcessRecord.Parameters), 220, x => x.Parameters);
        AddColumn(dataGrid, "说明", nameof(LegacyProcessRecord.Description), 260, x => x.Description);
        AddColumn(dataGrid, "状态", nameof(LegacyProcessRecord.Status), 120, x => x.Status);
    }

    private static void AddColumn<TValue>(AvaloniaDataGrid dataGrid, string header, string path, double width, Func<LegacyProcessRecord, TValue?> sortSelector)
    {
        dataGrid.Columns.Add(new DataGridTextColumn
        {
            Header = header,
            Width = new DataGridLength(width),
            SortMemberPath = path,
            Binding = new Binding(path),
            CustomSortComparer = new LegacyProcessRecordSortComparer<TValue>(sortSelector)
        });
    }

    private void SetTargetRowBackgroundButton_Click(object? sender, RoutedEventArgs e)
    {
        SetTargetRowBackground();
    }

    private void ClearTargetRowBackgroundButton_Click(object? sender, RoutedEventArgs e)
    {
        if (_viewModel is null)
        {
            return;
        }

        _viewModel.ClearTargetRowBackground();
        ReapplySorting();
        RefreshVisibleRows();
    }

    private void TargetValueTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            SetTargetRowBackground();
            e.Handled = true;
        }
    }

    private void SetTargetRowBackground()
    {
        if (_viewModel is null)
        {
            return;
        }

        _viewModel.TargetValueText = TargetValueTextBox.Text ?? string.Empty;
        if (_viewModel.SetTargetRowBackground())
        {
            ReapplySorting();
            RefreshVisibleRows();
        }
    }

    private void ReapplySorting()
    {
        if (CommonDataGrid.ItemsSource is DataGridCollectionView view)
        {
            view.Refresh();
        }
    }

    private void CommonDataGrid_LoadingRow(object? sender, DataGridRowEventArgs e)
    {
        ApplyRowBackground(e.Row);
    }

    private void RefreshVisibleRows()
    {
        if (_viewModel is null)
        {
            return;
        }

        foreach (var row in CommonDataGrid.GetVisualDescendants().OfType<DataGridRow>())
        {
            ApplyRowBackground(row);
        }
    }

    private void ApplyRowBackground(DataGridRow row)
    {
        if (row.DataContext is LegacyProcessRecord record && _viewModel?.ShouldHighlight(record) == true)
        {
            row.Background = HighlightBrush;
        }
        else
        {
            row.ClearValue(TemplatedControl.BackgroundProperty);
        }
    }
}
