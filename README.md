# CodeWF.AvaloniaControls.DataGrid

| 名称 | NuGet | 下载量 |
|------|-------|--------|
| CodeWF.AvaloniaControls.DataGrid | [![NuGet](https://img.shields.io/nuget/v/CodeWF.AvaloniaControls.DataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.DataGrid/) | [![NuGet](https://img.shields.io/nuget/dt/CodeWF.AvaloniaControls.DataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.DataGrid/) |

旧版免费 Avalonia DataGrid / TreeDataGrid 辅助包与可运行示例。

## 仓库规范

- 当前版本：`12.0.2.3`，版本号统一维护在根目录 `Directory.Build.props` 的 `<Version>` 节点。
- NuGet 包项目统一支持 `net8.0;net10.0`；Demo、App、测试与内部应用项目统一使用 `net11.0` / `net11.0-windows`。
- 根目录 `logo.svg`、`logo.png`、`logo.ico` 是唯一图标源，子工程只通过 MSBuild `Link` 引用，不维护图标副本。
- 运行时帮助、Markdown 示例、内置备忘录、设计说明等业务文档按功能保留；仓库级入口文档使用根目录 `README.md` 和 `UpdateLog.md`。

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
