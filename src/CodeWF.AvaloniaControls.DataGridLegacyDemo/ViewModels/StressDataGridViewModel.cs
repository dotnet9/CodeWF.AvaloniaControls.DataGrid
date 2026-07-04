using CodeWF.AvaloniaControls.DataGridLegacyDemo.Models;
using System.Collections.ObjectModel;

namespace CodeWF.AvaloniaControls.DataGridLegacyDemo.ViewModels;

public sealed class StressDataGridViewModel
{
    public StressDataGridViewModel(string header, int count, int seed)
    {
        Header = header;
        Count = count;
        Records = new ObservableCollection<LegacyProcessRecord>(LegacyProcessGridData.CreateStressRecords(count, seed));
    }

    public string Header { get; }

    public int Count { get; }

    public ObservableCollection<LegacyProcessRecord> Records { get; }
}
