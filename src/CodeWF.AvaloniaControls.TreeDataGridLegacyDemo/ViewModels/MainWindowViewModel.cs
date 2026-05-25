namespace CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.ViewModels;

public sealed class MainWindowViewModel
{
    public CommonTreeDataGridViewModel Common { get; } = new();

    public StressTreeDataGridViewModel ScenarioOne { get; } = new("六万行切换", 60000, 19);

    public StressTreeDataGridViewModel ScenarioTwo { get; } = new("十二万行切换", 120000, 31);

    public StressTreeDataGridViewModel ScenarioThree { get; } = new("十八万行切换", 180000, 43);
}
