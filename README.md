# CodeWF.AvaloniaControls.DataGrid

| 名称 | NuGet | 下载量 |
|------|-------|--------|
| CodeWF.AvaloniaControls.DataGrid | [![NuGet](https://img.shields.io/nuget/v/CodeWF.AvaloniaControls.DataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.DataGrid/) | [![NuGet](https://img.shields.io/nuget/dt/CodeWF.AvaloniaControls.DataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.DataGrid/) |
| CodeWF.AvaloniaControls.TreeDataGrid | [![NuGet](https://img.shields.io/nuget/v/CodeWF.AvaloniaControls.TreeDataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.TreeDataGrid/) | [![NuGet](https://img.shields.io/nuget/dt/CodeWF.AvaloniaControls.TreeDataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.TreeDataGrid/) |

Avalonia DataGrid / TreeDataGrid 辅助包与可运行示例。

## 仓库规范

- `CodeWF.AvaloniaControls.DataGrid` 当前版本：`12.0.1.2`。
- `CodeWF.AvaloniaControls.TreeDataGrid` 当前版本：`11.1.1.2`。
- 每个 NuGet 项目的包版本和依赖版本在各自 `.csproj` 中维护，避免 DataGrid、TreeDataGrid 和 Demo 之间的 Avalonia 版本线互相牵制。
- NuGet 包项目统一支持 `net8.0;net10.0`；Demo、App、测试与内部应用项目统一使用 `net11.0` / `net11.0-windows`。
- 根目录 `logo.svg`、`logo.png`、`logo.ico` 是唯一图标源，子工程只通过 MSBuild `Link` 引用，不维护图标副本。
- 运行时帮助、Markdown 示例、内置备忘录、设计说明等业务文档按功能保留；仓库级入口文档使用根目录 `README.md` 和 `UpdateLog.md`。

## 安装

```powershell
Install-Package CodeWF.AvaloniaControls.DataGrid
Install-Package CodeWF.AvaloniaControls.TreeDataGrid
```

## 快速启用

DataGrid 支持分别启用单项能力，也支持统一启用默认增强：

```csharp
dataGrid.AddSorting();
dataGrid.AddNaturalSorting();
dataGrid.EnableSmartTooltips();
dataGrid.EnableDefaults();
```

在 XAML 全局样式中也可以统一启用 DataGrid 默认增强：

```xml
<Style Selector="DataGrid">
  <Setter Property="(codewf:DataGridEnhancement.UseDefaults)" Value="True" />
</Style>
```

TreeDataGrid 需要传入 `FlatTreeDataGridSource<T>`，可以单项启用，也可以统一启用：

```csharp
treeDataGrid.AddSorting(source);
treeDataGrid.AddSelectAll(source);
treeDataGrid.EnableSmartTooltips();
treeDataGrid.EnableDefaults(source);
```

## 包线范围

`CodeWF.AvaloniaControls.DataGrid` 是 MIT 协议的免费开源 DataGrid 扩展包：

- `Avalonia.Controls.DataGrid` `12.0.1`
- `Semi.Avalonia.DataGrid` `12.0.0`

`CodeWF.AvaloniaControls.TreeDataGrid` 固定在旧版免费 TreeDataGrid 包线：

- `Avalonia.Controls.TreeDataGrid` `11.1.1`
- `Semi.Avalonia.TreeDataGrid` `11.1.1.1`

该包提供 TreeDataGrid 三态排序、全选、智能 ToolTip 和统一默认增强扩展方法。

高于该包线的官方 TreeDataGrid 版本涉及商业 License 授权，因此本仓库将 TreeDataGrid 扩展独立成单独 NuGet 并固定依赖版本。

## 仓库结构

- `src/CodeWF.AvaloniaControls.DataGrid`：DataGrid NuGet 类库
- `src/CodeWF.AvaloniaControls.TreeDataGrid`：TreeDataGrid NuGet 类库
- `src/CodeWF.AvaloniaControls.DataGridLegacyDemo`：DataGrid 压力示例
- `src/CodeWF.AvaloniaControls.TreeDataGridLegacyDemo`：旧版 TreeDataGrid 压力示例
- `CodeWF.AvaloniaControls.DataGrid.slnx`：独立解决方案

## 脚本

- `pack.bat`：还原、构建并打包两个 NuGet 类库到 `artifacts/packages`
- `publish_all.bat`：发布 DataGrid 和 TreeDataGrid 示例应用到 `publish/`
- `publishbase.bat`：`publish_all.bat` 使用的共享发布辅助脚本

## 说明

该仓库将 DataGrid 与 TreeDataGrid 扩展拆分为两条独立 NuGet 包线：DataGrid 跟随免费开源的 Avalonia 12 DataGrid 包，TreeDataGrid 固定在旧版免费包线以规避后续商业 License 约束。
