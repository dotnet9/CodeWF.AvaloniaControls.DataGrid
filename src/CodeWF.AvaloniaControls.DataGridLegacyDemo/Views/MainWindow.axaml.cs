using Avalonia.Controls;
using CodeWF.AvaloniaControls.DataGridLegacyDemo.ViewModels;

namespace CodeWF.AvaloniaControls.DataGridLegacyDemo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
