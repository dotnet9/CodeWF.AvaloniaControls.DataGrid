using CodeWF.AvaloniaControls.DataGridDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWF.AvaloniaControls.DataGridDemo.ViewModels;

internal static class LegacyProcessGridData
{
    public static List<LegacyProcessRecord> CreateCommonRecords(int count)
    {
        var items = new List<LegacyProcessRecord>(count);
        var nameNumbers = CreateShuffledNumbers(count, 20260704);
        var random = new Random(20260705);

        for (var i = 1; i <= count; i++)
        {
            var groupNo = i % 5 + 1;
            var lineNo = random.Next(1, 999);
            items.Add(new LegacyProcessRecord
            {
                Id = i,
                Name = $"普通任务 {nameNumbers[i - 1]}",
                Line = $"南区产线 {lineNo}",
                Host = $"10.40.{groupNo}.{i % 220 + 10}",
                ProgramPath = $@"D:\runtime\common\worker-{i % 8}.exe",
                WorkPath = $@"D:\runtime\common\workspace-{i % 12}",
                Parameters = i % 3 == 0 ? "--mode manual --trace" : "--mode auto --retry 1",
                Description = "用于验证 DataGrid 三态排序、智能提示和行状态设置。",
                Status = i % 6 == 0 ? "维护中" : "运行中"
            });
        }

        return items;
    }

    public static List<LegacyProcessRecord> CreateStressRecords(int count, int seed)
    {
        var items = new List<LegacyProcessRecord>(count);
        var nameNumbers = CreateShuffledNumbers(count, seed + 4096);
        var random = new Random(seed + 8192);

        // 这里故意一次性准备大数据量，用来稳定复现页签切换时的 UI 压力。
        for (var i = 1; i <= count; i++)
        {
            var groupNo = (i + seed) % 6 + 1;
            var lineNo = random.Next(1, 2000);
            items.Add(new LegacyProcessRecord
            {
                Id = i,
                Name = $"配方同步任务 {nameNumbers[i - 1]}",
                Line = $"东区产线 {lineNo}",
                Host = $"10.24.{seed % 20}.{i % 220 + 10}",
                ProgramPath = $@"D:\runtime\line-{groupNo}\worker-{i % 12}.exe",
                WorkPath = $@"D:\runtime\line-{groupNo}\workspace-{i % 18}",
                Parameters = i % 4 == 0 ? "--mode verify --delay 3" : "--mode sync --retry 2",
                Description = i % 5 == 0
                    ? "用于模拟工艺配方下发与回滚校验的大表格切换场景。"
                    : "用于观察 DataGrid 在多页签之间切换时的滚动与重绘负载。",
                Status = i % 7 == 0 ? "待复核" : "已就绪"
            });
        }

        return items;
    }

    private static List<int> CreateShuffledNumbers(int count, int seed)
    {
        var numbers = Enumerable.Range(1, count).ToList();
        var random = new Random(seed);

        for (var i = numbers.Count - 1; i > 0; i--)
        {
            var targetIndex = random.Next(i + 1);
            (numbers[i], numbers[targetIndex]) = (numbers[targetIndex], numbers[i]);
        }

        var firstValueIndex = numbers.IndexOf(1);
        if (firstValueIndex > 0)
        {
            (numbers[0], numbers[firstValueIndex]) = (numbers[firstValueIndex], numbers[0]);
        }

        return numbers;
    }
}
