using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using CodeWF.AvaloniaControls;
using CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.Models;
using CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.ViewModels;
using System.Linq;

namespace CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.Views;

public partial class CommonTreeDataGridView : UserControl
{
    private static readonly IBrush HighlightBrush = SolidColorBrush.Parse("#E5E7EB");
    private CommonTreeDataGridViewModel? _viewModel;

    public CommonTreeDataGridView()
    {
        InitializeComponent();

        CommonTreeDataGrid.RowPrepared += CommonTreeDataGrid_RowPrepared;
        CommonTreeDataGrid.RowClearing += CommonTreeDataGrid_RowClearing;
        DataContextChanged += (_, _) => ConfigureSource();
    }

    private void ConfigureSource()
    {
        if (DataContext is not CommonTreeDataGridViewModel viewModel)
        {
            _viewModel = null;
            CommonTreeDataGrid.Source = null;
            return;
        }

        if (ReferenceEquals(_viewModel, viewModel))
        {
            return;
        }

        _viewModel = viewModel;
        CommonTreeDataGrid.Source = viewModel.Source;
        CommonTreeDataGrid.AddSorting(viewModel.Source);
        CommonTreeDataGrid.AddSelectAll(viewModel.Source);
        RefreshVisibleRows();
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
            RefreshVisibleRows();
        }
    }

    private void CommonTreeDataGrid_RowPrepared(object? sender, TreeDataGridRowEventArgs e)
    {
        ApplyRowBackground(e.Row);
    }

    private void CommonTreeDataGrid_RowClearing(object? sender, TreeDataGridRowEventArgs e)
    {
        e.Row.ClearValue(TemplatedControl.BackgroundProperty);
    }

    private void RefreshVisibleRows()
    {
        if (_viewModel is null)
        {
            return;
        }

        foreach (var row in CommonTreeDataGrid.GetVisualDescendants().OfType<TreeDataGridRow>())
        {
            ApplyRowBackground(row);
        }
    }

    private void ApplyRowBackground(TreeDataGridRow row)
    {
        if (row.Model is LegacyTreeRecord record && _viewModel?.ShouldHighlight(record) == true)
        {
            row.Background = HighlightBrush;
        }
        else
        {
            row.ClearValue(TemplatedControl.BackgroundProperty);
        }
    }
}
