using Avalonia.Controls;
using CodeWF.AvaloniaControls.DataGridDemo.ViewModels;

namespace CodeWF.AvaloniaControls.DataGridDemo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
