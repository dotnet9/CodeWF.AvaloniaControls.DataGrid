# 更新日志

## 12.0.1.1 / 11.1.1.1 (2026-07-04)

- 🔨[优化]-拆分为 `CodeWF.AvaloniaControls.DataGrid` 和 `CodeWF.AvaloniaControls.TreeDataGrid` 两个 NuGet 包。
- 🔨[优化]-`CodeWF.AvaloniaControls.DataGrid` 升级到 `12.0.1.1`，仅引用 `Avalonia.Controls.DataGrid` `12.0.1` 和 `Semi.Avalonia.DataGrid` `12.0.0`。
- 🔨[优化]-`CodeWF.AvaloniaControls.TreeDataGrid` 固定为 `11.1.1.1`，仅引用 `Avalonia.Controls.TreeDataGrid` `11.1.1` 和 `Semi.Avalonia.TreeDataGrid` `11.1.1.1`。
- 🔨[优化]-移除根目录 `Directory.Packages.props`，改为在各项目 `.csproj` 中显式维护依赖版本，便于 DataGrid、TreeDataGrid 和 Demo 分别固定不同 Avalonia 版本线。
- 😄[新增]-为 `TreeDataGridExtension` 增加 `EnableSmartTooltips` 扩展方法，支持仅在文本显示不全时显示 ToolTip。
- 🔤[优化]-TreeDataGrid 示例字符串列改用自然排序，避免 `普通节点 100` 排在 `普通节点 11` 前面。
- 🔤[优化]-DataGrid 示例字符串列改用自然排序，并修正自定义排序比较器下的三态取消排序识别。
- 🔨[优化]-TreeDataGrid 三态排序注册改为幂等，避免重复注册后第三次点击取消排序又触发升序排序。
- 🔤[优化]-DataGrid 通用示例的灰色目标行加入排序置顶优先级，设置或清除后会刷新当前排序视图。

## 12.0.2.3 (2026-06-08)

- 🔨[优化]-补齐根目录 logo.svg、logo.png、logo.ico 三件套，子工程通过 MSBuild Link 引用根 logo，避免维护多份图标副本。
- 🔨[优化]-统一目标框架：NuGet 包项目支持 `net8.0;net10.0`，Demo、App、测试与内部应用项目升级到 `net11.0` / `net11.0-windows`。
- 🔨[优化]-保留运行时帮助、Markdown 示例、内置备忘录和业务设计文档，仅收敛仓库级重复文档入口。

## 12.0.2.2 (2026-06-08)

- 保留根目录 `Directory.Build.props` 作为通用打包元数据入口，包版本改由各 NuGet 项目单独维护。
- 清理英文/双语文档入口，后续仅维护简体中文文档。
- 完善 NuGet 发布配置，补充 Source Link、符号包和标签格式规范。


## 12.0.2.1 - 2026-05-08

- Migrated `CodeWF.AvaloniaControls.DataGrid`, `CodeWF.AvaloniaControls.DataGridLegacyDemo`, and `CodeWF.AvaloniaControls.TreeDataGridLegacyDemo` from the main `CodeWF.AvaloniaControls` repository.
- Added standalone solution, pack script, publish script, and repository metadata for the legacy free DataGrid / TreeDataGrid package line.

## 11.2.1.9 - 2025-07-15

- Added `CodeWF.AvaloniaControls.DataGrid`.

---

## 归档：src\CodeWF.AvaloniaControls.DataGrid\UpdateLog.md

# 更新日志

V12.0.2（2026-05-02）

- 😄[新增]-新增当前工程独立更新日志文件，后续 `CodeWF.AvaloniaControls.DataGrid` 的变更改为在工程目录内持续记录
- 😄[新增]-保留最后一个免费开源官方 `Avalonia.Controls.DataGrid` 与 `Avalonia.Controls.TreeDataGrid` 兼容链路，便于继续对外分发旧版扩展包
- 🔤[优化]-将当前工程改为显式固定旧版兼容依赖，不再走中央包管理，避免与 Avalonia 12 主线示例产生版本牵制
- 🔤[优化]-整理并保留 `DataGrid` 三态排序、智能提示，以及 `TreeDataGrid` 三态排序与全选扩展，方便示例和业务项目复用

---

## 归档：src\CodeWF.AvaloniaControls.DataGridLegacyDemo\UpdateLog.md

# 更新日志

V12.0.2（2026-05-02）

- 😄[新增]-新增当前工程独立更新日志文件，后续 `CodeWF.AvaloniaControls.DataGridLegacyDemo` 的变更改为在工程目录内持续记录
- 😄[新增]-新增独立旧版免费 `DataGrid` 专项示例工程，用于承载最后一个免费开源版本链路
- 😄[新增]-新增大数据量 `TabControl` 切换演示场景，便于直观看到旧版 `DataGrid` 在多页签之间切换时的卡顿与重绘压力
- 🔤[优化]-统一补充中文界面文案，并接入 `CodeWF.AvaloniaControls.DataGrid` 扩展方法用于三态排序与智能提示展示

---

## 归档：src\CodeWF.AvaloniaControls.TreeDataGridLegacyDemo\UpdateLog.md

# 更新日志

V12.0.2（2026-05-02）

- 😄[新增]-新增当前工程独立更新日志文件，后续 `CodeWF.AvaloniaControls.TreeDataGridLegacyDemo` 的变更改为在工程目录内持续记录
- 😄[新增]-新增独立旧版免费 `TreeDataGrid` 专项示例工程，用于承载最后一个免费开源版本链路
- 😄[新增]-新增大数据量 `TabControl` 切换演示场景，便于与旧版 `DataGrid` 做直观的切换流畅度对照
- 🔤[优化]-接入 `CodeWF.AvaloniaControls.DataGrid` 中的 `TreeDataGrid` 扩展方法，并统一整理为中文界面文案
## 2026-06-08 仓库规范整理

- 统一文档维护入口：每个仓库只保留根目录 `README.md` 和根目录 `UpdateLog.md`，清理重复日志、英文文档和语言切换入口。
- 版本维护入口：通用打包元数据保留在根目录 `Directory.Build.props`，包版本由各 NuGet 项目单独维护。
- 不再维护 `global.json`，SDK 选择交给本机或 CI 环境；NuGet 包和应用的目标框架在项目文件中明确声明。
- 统一 NuGet 包文档入口：包 README 统一引用仓库根 `README.md`，更新日志统一引用仓库根 `UpdateLog.md`。
