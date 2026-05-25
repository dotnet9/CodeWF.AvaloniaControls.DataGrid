using Avalonia.Controls;
using CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.Models;
using System.Collections.ObjectModel;

namespace CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.ViewModels;

public sealed class StressTreeDataGridViewModel
{
    public StressTreeDataGridViewModel(string header, int count, int seed)
    {
        Header = header;
        Count = count;

        var records = new ObservableCollection<LegacyTreeRecord>(LegacyTreeGridData.CreateStressRecords(count, seed));
        Source = LegacyTreeGridData.CreateSource(records);
    }

    public string Header { get; }

    public int Count { get; }

    public FlatTreeDataGridSource<LegacyTreeRecord> Source { get; }
}
