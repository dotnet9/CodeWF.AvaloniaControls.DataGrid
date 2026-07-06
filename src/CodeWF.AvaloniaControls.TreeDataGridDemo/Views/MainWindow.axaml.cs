using Avalonia.Controls;
using CodeWF.AvaloniaControls.TreeDataGridDemo.ViewModels;

namespace CodeWF.AvaloniaControls.TreeDataGridDemo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
