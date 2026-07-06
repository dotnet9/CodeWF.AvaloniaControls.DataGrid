namespace CodeWF.AvaloniaControls.DataGridDemo.ViewModels;

public sealed class MainWindowViewModel
{
    public CommonDataGridViewModel Common { get; } = new();

    public StressDataGridViewModel ScenarioOne { get; } = new("六万行切换", 60000, 17);

    public StressDataGridViewModel ScenarioTwo { get; } = new("十二万行切换", 120000, 29);

    public StressDataGridViewModel ScenarioThree { get; } = new("十八万行切换", 180000, 41);
}
