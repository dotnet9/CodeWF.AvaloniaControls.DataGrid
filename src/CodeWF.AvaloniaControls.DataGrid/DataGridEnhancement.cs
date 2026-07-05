using Avalonia;
using Avalonia.Controls;

namespace CodeWF.AvaloniaControls;

public sealed class DataGridEnhancement
{
    public static readonly AttachedProperty<bool> UseDefaultsProperty =
        AvaloniaProperty.RegisterAttached<DataGridEnhancement, DataGrid, bool>("UseDefaults");

    static DataGridEnhancement()
    {
        UseDefaultsProperty.Changed.AddClassHandler<DataGrid>(OnUseDefaultsChanged);
    }

    private DataGridEnhancement()
    {
    }

    public static bool GetUseDefaults(DataGrid dataGrid)
    {
        return dataGrid.GetValue(UseDefaultsProperty);
    }

    public static void SetUseDefaults(DataGrid dataGrid, bool value)
    {
        dataGrid.SetValue(UseDefaultsProperty, value);
    }

    private static void OnUseDefaultsChanged(DataGrid dataGrid, AvaloniaPropertyChangedEventArgs args)
    {
        if (args.GetNewValue<bool>())
        {
            dataGrid.EnableDefaults();
        }
    }
}
