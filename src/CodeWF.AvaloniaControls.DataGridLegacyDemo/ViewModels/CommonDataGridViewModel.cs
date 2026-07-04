using CodeWF.AvaloniaControls.DataGridLegacyDemo.Models;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace CodeWF.AvaloniaControls.DataGridLegacyDemo.ViewModels;

public sealed class CommonDataGridViewModel : ViewModelBase
{
    private const int RecordCount = 200;
    private const string DefaultTargetName = "普通任务 1";
    private int? _highlightedId;
    private string? _highlightedName = DefaultTargetName;

    public CommonDataGridViewModel()
    {
        Records = new ObservableCollection<LegacyProcessRecord>(LegacyProcessGridData.CreateCommonRecords(RecordCount));
    }

    public string Header { get; } = "通用示例";

    public ObservableCollection<LegacyProcessRecord> Records { get; }

    public int TargetFieldIndex
    {
        get;
        set => SetProperty(ref field, value);
    } = 1;

    public string TargetValueText
    {
        get;
        set => SetProperty(ref field, value);
    } = DefaultTargetName;

    public string ResultText
    {
        get;
        private set => SetProperty(ref field, value);
    } = $"任务名称 {DefaultTargetName} 已设置为灰色并置顶";

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
        BringHighlightedRecordToTop();
        ResultText = $"编号 {id} 已设置为灰色并置顶";
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
        BringHighlightedRecordToTop();
        ResultText = $"任务名称 {name} 已设置为灰色并置顶";
        return true;
    }

    public void ClearTargetRowBackground()
    {
        _highlightedId = null;
        _highlightedName = null;
        RestoreDefaultRecordOrder();
        ResultText = "已清除";
    }

    public bool ShouldHighlight(LegacyProcessRecord record)
    {
        return record.Id == _highlightedId ||
            (_highlightedName is not null &&
             string.Equals(record.Name, _highlightedName, StringComparison.Ordinal));
    }

    private void BringHighlightedRecordToTop()
    {
        var record = Records.FirstOrDefault(ShouldHighlight);
        if (record is null)
        {
            return;
        }

        var index = Records.IndexOf(record);
        if (index > 0)
        {
            Records.Move(index, 0);
        }
    }

    private void RestoreDefaultRecordOrder()
    {
        var orderedRecords = Records.OrderBy(x => x.Id).ToList();
        for (var targetIndex = 0; targetIndex < orderedRecords.Count; targetIndex++)
        {
            var currentIndex = Records.IndexOf(orderedRecords[targetIndex]);
            if (currentIndex != targetIndex)
            {
                Records.Move(currentIndex, targetIndex);
            }
        }
    }
}
