using Avalonia.Controls;
using CodeWF.AvaloniaControls;
using CodeWF.AvaloniaControls.TreeDataGridDemo.ViewModels;

namespace CodeWF.AvaloniaControls.TreeDataGridDemo.Views;

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
        StressTreeDataGrid.EnableDefaults(viewModel.Source);
    }
}
