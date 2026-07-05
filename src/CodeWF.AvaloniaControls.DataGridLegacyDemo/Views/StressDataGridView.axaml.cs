using Avalonia.Controls;
using Avalonia.Data;
using CodeWF.AvaloniaControls.DataGridLegacyDemo.Models;
using CodeWF.AvaloniaControls.DataGridLegacyDemo.ViewModels;
using System;

namespace CodeWF.AvaloniaControls.DataGridLegacyDemo.Views;

public partial class StressDataGridView : UserControl
{
    private StressDataGridViewModel? _viewModel;

    public StressDataGridView()
    {
        InitializeComponent();

        ConfigureDataGrid(StressDataGrid);
        DataContextChanged += (_, _) => ConfigureSource();
    }

    private void ConfigureSource()
    {
        if (DataContext is not StressDataGridViewModel viewModel)
        {
            _viewModel = null;
            StressDataGrid.ItemsSource = null;
            return;
        }

        if (ReferenceEquals(_viewModel, viewModel))
        {
            return;
        }

        _viewModel = viewModel;
        StressDataGrid.ItemsSource = viewModel.Records;
        StressDataGrid.EnableDefaults();
    }

    private static void ConfigureDataGrid(DataGrid dataGrid)
    {
        EnsureColumns(dataGrid);
        dataGrid.RowHeight = 36;
        dataGrid.CanUserReorderColumns = false;
        dataGrid.CanUserResizeColumns = true;
        dataGrid.GridLinesVisibility = DataGridGridLinesVisibility.Horizontal;
    }

    private static void EnsureColumns(DataGrid dataGrid)
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

    private static void AddColumn<TValue>(DataGrid dataGrid, string header, string path, double width, Func<LegacyProcessRecord, TValue?> sortSelector)
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
}
