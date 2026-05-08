# CodeWF.AvaloniaControls.DataGrid

| 名称 | NuGet | 下载量 |
|------|-------|--------|
| CodeWF.AvaloniaControls.DataGrid | [![NuGet](https://img.shields.io/nuget/v/CodeWF.AvaloniaControls.DataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.DataGrid/) | [![NuGet](https://img.shields.io/nuget/dt/CodeWF.AvaloniaControls.DataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.DataGrid/) |

旧版免费 Avalonia DataGrid / TreeDataGrid 辅助包与可运行示例。

[English](README.md) | 简体中文

## 安装

```powershell
Install-Package CodeWF.AvaloniaControls.DataGrid
```

## 包线范围

`CodeWF.AvaloniaControls.DataGrid` 保持在最后一个免费官方 Avalonia 表格包链路：

- `Avalonia.Controls.DataGrid` `11.3.7`
- `Avalonia.Controls.TreeDataGrid` `11.1.1`
- `Semi.Avalonia.DataGrid` `11.3.7.3`
- `Semi.Avalonia.TreeDataGrid` `11.1.1.1`

该包提供 DataGrid 三态排序、智能 ToolTip、TreeDataGrid 三态排序、TreeDataGrid 全选等扩展方法。

## 仓库结构

- `src/CodeWF.AvaloniaControls.DataGrid`：NuGet 类库
- `src/CodeWF.AvaloniaControls.DataGridLegacyDemo`：旧版 DataGrid 压力示例
- `src/CodeWF.AvaloniaControls.TreeDataGridLegacyDemo`：旧版 TreeDataGrid 压力示例
- `CodeWF.AvaloniaControls.DataGrid.slnx`：独立解决方案

## 脚本

- `pack.bat`：还原、构建并打包 DataGrid 类库到 `artifacts/packages`
- `publish_all.bat`：发布 DataGrid 和 TreeDataGrid 示例应用到 `publish/`
- `publishbase.bat`：`publish_all.bat` 使用的共享发布辅助脚本

## 说明

该仓库从 Avalonia 12 主线控件仓库中独立出来，便于旧版免费表格包继续保持显式依赖版本，避免在主仓库中央包管理中增加兼容分支。
