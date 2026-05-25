using Avalonia.Controls;
using CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.Models;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.ViewModels;

public sealed class CommonTreeDataGridViewModel : ViewModelBase
{
    private const int RecordCount = 200;
    private string _resultText = $"共 {RecordCount:N0} 行";
    private string _targetValueText = "普通节点 1";
    private int _targetFieldIndex = 1;
    private int? _highlightedId;
    private string? _highlightedName;

    public CommonTreeDataGridViewModel()
    {
        Records = new ObservableCollection<LegacyTreeRecord>(LegacyTreeGridData.CreateCommonRecords(RecordCount));
        Source = LegacyTreeGridData.CreateSource(Records);
    }

    public string Header { get; } = "通用示例";

    public ObservableCollection<LegacyTreeRecord> Records { get; }

    public FlatTreeDataGridSource<LegacyTreeRecord> Source { get; }

    public int TargetFieldIndex
    {
        get => _targetFieldIndex;
        set => SetProperty(ref _targetFieldIndex, value);
    }

    public string TargetValueText
    {
        get => _targetValueText;
        set => SetProperty(ref _targetValueText, value);
    }

    public string ResultText
    {
        get => _resultText;
        private set => SetProperty(ref _resultText, value);
    }

    public bool SetTargetRowBackground()
    {
        var text = TargetValueText.Trim();
        if (string.IsNullOrWhiteSpace(text))
        {
            ResultText = "请输入匹配值";
            return false;
        }

        if (TargetFieldIndex == 0)
        {
            return SetTargetRowBackgroundById(text);
        }

        return SetTargetRowBackgroundByName(text);
    }

    private bool SetTargetRowBackgroundById(string text)
    {
        if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var id))
        {
            ResultText = "请输入有效编号";
            return false;
        }

        if (Records.All(x => x.Id != id))
        {
            ResultText = $"未找到编号 {id}";
            return false;
        }

        _highlightedId = id;
        _highlightedName = null;
        ResultText = $"编号 {id} 已设置为灰色";
        return true;
    }

    private bool SetTargetRowBackgroundByName(string name)
    {
        if (Records.All(x => !string.Equals(x.Name, name, StringComparison.Ordinal)))
        {
            ResultText = $"未找到任务名称 {name}";
            return false;
        }

        _highlightedId = null;
        _highlightedName = name;
        ResultText = $"任务名称 {name} 已设置为灰色";
        return true;
    }

    public void ClearTargetRowBackground()
    {
        _highlightedId = null;
        _highlightedName = null;
        ResultText = "已清除";
    }

    public bool ShouldHighlight(LegacyTreeRecord record)
    {
        return record.Id == _highlightedId ||
            (_highlightedName is not null &&
             string.Equals(record.Name, _highlightedName, StringComparison.Ordinal));
    }
}
