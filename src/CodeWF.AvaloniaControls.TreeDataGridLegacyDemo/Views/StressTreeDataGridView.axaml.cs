using Avalonia.Controls;
using CodeWF.AvaloniaControls;
using CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.ViewModels;

namespace CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.Views;

public partial class StressTreeDataGridView : UserControl
{
    private StressTreeDataGridViewModel? _viewModel;

    public StressTreeDataGridView()
    {
        InitializeComponent();
        DataContextChanged += (_, _) => ConfigureSource();
    }

    private void ConfigureSource()
    {
        if (DataContext is not StressTreeDataGridViewModel viewModel)
        {
            _viewModel = null;
            StressTreeDataGrid.Source = null;
            return;
        }

        if (ReferenceEquals(_viewModel, viewModel))
        {
            return;
        }

        _viewModel = viewModel;
        StressTreeDataGrid.Source = viewModel.Source;
        StressTreeDataGrid.AddSorting(viewModel.Source);
        StressTreeDataGrid.AddSelectAll(viewModel.Source);
    }
}
