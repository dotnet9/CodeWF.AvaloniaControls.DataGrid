using Avalonia.Controls;
using CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.ViewModels;

namespace CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
