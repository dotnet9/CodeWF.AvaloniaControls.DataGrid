> [!WARNING]
> # ⚠️ 本仓库已停止维护 / This repository is no longer maintained
>
> 本仓库已归档：不再接受 Issue 与 Pull Request，NuGet 旧包保留但不再升级。
>
> This repository is archived: issues and pull requests are closed, and existing NuGet packages will not receive updates.
>
> **替代方案 / Replacement:**
>
> | 旧包 / Old packages | 新包 / New packages |
> |---|---|
> | CodeWF.AvaloniaControls.DataGrid(.Themes) / TreeDataGrid(.Themes) | CodeWF.Avalonia.DataGrid / CodeWF.Avalonia.TreeDataGrid |
>
> 新仓库 / New repository: **https://github.com/dotnet9/CodeWF.Avalonia**

# CodeWF.AvaloniaControls.DataGrid

| 名称 | NuGet | 下载量 |
|------|-------|--------|
| CodeWF.AvaloniaControls.DataGrid | [![NuGet](https://img.shields.io/nuget/v/CodeWF.AvaloniaControls.DataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.DataGrid/) | [![NuGet](https://img.shields.io/nuget/dt/CodeWF.AvaloniaControls.DataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.DataGrid/) |
| CodeWF.AvaloniaControls.DataGrid.Themes | [![NuGet](https://img.shields.io/nuget/v/CodeWF.AvaloniaControls.DataGrid.Themes.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.DataGrid.Themes/) | [![NuGet](https://img.shields.io/nuget/dt/CodeWF.AvaloniaControls.DataGrid.Themes.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.DataGrid.Themes/) |
| CodeWF.AvaloniaControls.TreeDataGrid | [![NuGet](https://img.shields.io/nuget/v/CodeWF.AvaloniaControls.TreeDataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.TreeDataGrid/) | [![NuGet](https://img.shields.io/nuget/dt/CodeWF.AvaloniaControls.TreeDataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.TreeDataGrid/) |
| CodeWF.AvaloniaControls.TreeDataGrid.Themes | [![NuGet](https://img.shields.io/nuget/v/CodeWF.AvaloniaControls.TreeDataGrid.Themes.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.TreeDataGrid.Themes/) | [![NuGet](https://img.shields.io/nuget/dt/CodeWF.AvaloniaControls.TreeDataGrid.Themes.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.TreeDataGrid.Themes/) |

Avalonia DataGrid / TreeDataGrid 辅助包与可运行示例。

## 仓库规范

- `CodeWF.AvaloniaControls.DataGrid` 与 `CodeWF.AvaloniaControls.DataGrid.Themes` 当前版本：`12.1.2.10`。
- `CodeWF.AvaloniaControls.TreeDataGrid` 与 `CodeWF.AvaloniaControls.TreeDataGrid.Themes` 当前版本：`11.1.1.16`。
- NuGet 包项目统一支持 `net8.0;net10.0;net11.0`；Demo 项目使用 `net11.0` / `net11.0-windows`。
- DataGrid 与 TreeDataGrid 的 Avalonia 依赖版本在根目录 `Directory.Packages.props` 中按包线集中维护。
- 根目录 `logo.svg`、`logo.png`、`logo.ico` 是唯一图标源，子工程只通过 MSBuild `Link` 引用，不维护图标副本。
- 运行时帮助、Markdown 示例、内置备忘录、设计说明等业务文档按功能保留；仓库级入口文档使用根目录 `README.md` 和 `UpdateLog.md`。

## 安装

```powershell
Install-Package CodeWF.AvaloniaControls.DataGrid
Install-Package CodeWF.AvaloniaControls.DataGrid.Themes
Install-Package CodeWF.AvaloniaControls.TreeDataGrid
Install-Package CodeWF.AvaloniaControls.TreeDataGrid.Themes
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

- `Avalonia.Controls.DataGrid` `12.1.2`
- `Semi.Avalonia.DataGrid` `12.1.0.1`
- `Semi.Avalonia` `12.1.0.1`

`CodeWF.AvaloniaControls.TreeDataGrid` 固定在旧版免费 TreeDataGrid 包线：

- `Avalonia.Controls.TreeDataGrid` `11.1.1`
- `Semi.Avalonia.TreeDataGrid` `11.1.1.1`
- `Semi.Avalonia` `11.3.7`

该包提供 TreeDataGrid 三态排序、全选、智能 ToolTip 和统一默认增强扩展方法。

高于该包线的官方 TreeDataGrid 版本涉及商业 License 授权，因此本仓库将 TreeDataGrid 扩展独立成单独 NuGet 并固定依赖版本。

## 仓库结构

- `src/CodeWF.AvaloniaControls.DataGrid`：DataGrid NuGet 类库
- `src/CodeWF.AvaloniaControls.DataGrid.Themes`：DataGrid Semi 主题 NuGet 类库
- `src/CodeWF.AvaloniaControls.TreeDataGrid`：TreeDataGrid NuGet 类库
- `src/CodeWF.AvaloniaControls.TreeDataGrid.Themes`：TreeDataGrid Semi 主题 NuGet 类库
- `src/CodeWF.AvaloniaControls.DataGridDemo`：DataGrid 压力示例
- `src/CodeWF.AvaloniaControls.TreeDataGridDemo`：TreeDataGrid 压力示例
- `tests/CodeWF.AvaloniaControls.Tests`：DataGrid 纯逻辑回归测试
- `CodeWF.AvaloniaControls.DataGrid.slnx`：独立解决方案

## 脚本

- `pack.bat`：还原、构建并打包 NuGet 类库到 `artifacts/packages`
- `publish_all.bat`：发布 DataGrid 和 TreeDataGrid 示例应用到 `publish/`
- `publishbase.bat`：`publish_all.bat` 使用的共享发布辅助脚本

## 说明

该仓库将 DataGrid 与 TreeDataGrid 扩展拆分为两条独立 NuGet 包线：DataGrid 跟随免费开源的 Avalonia 12 DataGrid 包，TreeDataGrid 固定在旧版免费包线以规避后续商业 License 约束。

## CI/CD：自动发布 NuGet 包

推送 `v*` 标签（例如 `v12.1.2.12`）会触发 [.github/workflows/publish-nuget.yml](.github/workflows/publish-nuget.yml)，一次发布四个包：`CodeWF.AvaloniaControls.DataGrid`、`CodeWF.AvaloniaControls.DataGrid.Themes`、`CodeWF.AvaloniaControls.TreeDataGrid`、`CodeWF.AvaloniaControls.TreeDataGrid.Themes`（含 snupkg 符号包），完成后自动创建 GitHub Release。

本仓库有两条独立版本线（`Directory.Build.props` 的 `DataGridPackageVersion` 与 `TreeDataGridPackageVersion`），标签以 DataGrid 版本为参照；**打标签前先把本次要发布的版本线升好**——nuget.org 拒绝重复的版本号，未升版本的包会被 `--skip-duplicate` 自动跳过。认证使用 NuGet Trusted Publishing：工作流通过 `nuget/login@v1` 以 OIDC 令牌换取一次性发布凭据，仓库不保存任何 secret。nuget.org 侧的 API key 需绑定本仓库与工作流文件名 `publish-nuget.yml`（Scopes 勾选 Push，Glob 建议收窄为 `CodeWF.*`）；这类 key 创建后 7 天内需成功发布一次才会转永久有效。

手工发布步骤：

```bash
# 1. 升版本：修改 Directory.Build.props 中对应版本线并提交
# 2. 打标签（用 DataGrid 版本号）并推送，触发发布
git tag -a v12.1.2.13 -m "CodeWF.AvaloniaControls.DataGrid v12.1.2.13"
git push origin v12.1.2.13
# 3. 在 GitHub Actions 观察 publish-nuget 运行，完成后到 nuget.org 核对四个包
```
