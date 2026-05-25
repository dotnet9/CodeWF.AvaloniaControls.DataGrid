using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeWF.AvaloniaControls.TreeDataGridLegacyDemo.ViewModels;

internal static class LegacyTreeGridData
{
    public static FlatTreeDataGridSource<LegacyTreeRecord> CreateSource(ObservableCollection<LegacyTreeRecord> records)
    {
        return new FlatTreeDataGridSource<LegacyTreeRecord>(records)
        {
            Columns =
            {
                new TextColumn<LegacyTreeRecord, int>("编号", x => x.Id),
                new TextColumn<LegacyTreeRecord, string>("任务名称", x => x.Name),
                new TextColumn<LegacyTreeRecord, string>("产线", x => x.Line),
                new TextColumn<LegacyTreeRecord, string>("主机", x => x.Host),
                new TextColumn<LegacyTreeRecord, string>("模式", x => x.Mode),
                new TextColumn<LegacyTreeRecord, string>("责任人", x => x.Owner),
                new TextColumn<LegacyTreeRecord, string>("状态", x => x.Status),
            }
        };
    }

    public static List<LegacyTreeRecord> CreateCommonRecords(int count)
    {
        var items = new List<LegacyTreeRecord>(count);

        for (var i = 1; i <= count; i++)
        {
            var lineNo = i % 5 + 1;
            items.Add(new LegacyTreeRecord
            {
                Id = i,
                Name = $"普通节点 {i}",
                Line = $"南区 {lineNo} 号线",
                Host = $"10.40.{lineNo}.{i % 220 + 10}",
                Mode = i % 3 == 0 ? "手动" : "自动",
                Owner = i % 4 == 0 ? "现场班组" : "调度中心",
                Status = i % 6 == 0 ? "维护中" : "运行中"
            });
        }

        return items;
    }

    public static List<LegacyTreeRecord> CreateStressRecords(int count, int seed)
    {
        var items = new List<LegacyTreeRecord>(count);

        for (var i = 1; i <= count; i++)
        {
            var lineNo = (i + seed) % 6 + 1;
            items.Add(new LegacyTreeRecord
            {
                Id = i,
                Name = $"工艺节点 {i}",
                Line = $"东区 {lineNo} 号线",
                Host = $"10.30.{seed % 18}.{i % 220 + 10}",
                Mode = i % 4 == 0 ? "巡检" : "同步",
                Owner = i % 5 == 0 ? "夜班值守" : "调度中心",
                Status = i % 7 == 0 ? "待处理" : "运行中"
            });
        }

        return items;
    }
}
