# 更新日志

## 12.1.2.8 / 11.1.1.14 (2026-09-22)

- 🛡️[安全]-中央依赖固定 `Tmds.DBus.Protocol` `0.95.1`，消除 Avalonia 11 依赖线中的高危漏洞告警，不升级 TreeDataGrid 免费包线。

## 12.1.2.7 / 11.1.1.13 (2026-09-22)

- 🐛[修复]-智能 ToolTip 不再覆盖宿主 `TextBlock.Tag`，改用弱表登记控件状态，避免重复订阅和宿主业务数据冲突。
- 🐛[修复]-延迟处理 ToolTip 前检查行仍挂载在视觉树中，避免回收行被异步任务继续处理。
- 🛠️[优化]-将 ToolTip 更新中的广义空异常处理收窄为可预期的参数异常，并在异常时清除 ToolTip。

## 12.1.2.6 / 11.1.1.12 (2026-09-22)

- 🐛[修复]-DataGrid 和 TreeDataGrid 的三态排序状态与当前数据视图绑定，替换 `ItemsSource` 或 `Source` 后首次排序从升序重新开始。

## 12.1.2.5 / 11.1.1.11 (2026-09-22)

- 🐛[修复]-DataGrid 和 TreeDataGrid 的增强事件改为控件级幂等注册，避免重复调用扩展方法导致重复处理。
- 🐛[修复]-TreeDataGrid 全选操作始终读取控件当前数据源，支持同类型数据源重绑，并确保批量选择结束时释放更新状态。
- 🐛[修复]-TreeDataGrid 编辑文本框时不拦截 Ctrl+A，保留文本编辑器原生全选行为。

## 12.1.2.4 / 11.1.1.10 (2026-09-22)

- 🛠️[优化]-移除 DataGrid 排序对 Avalonia 内部排序方法的运行时反射，改用公开排序路径以支持 NativeAOT。
- 🛠️[优化]-TreeDataGrid 单元格列索引改用公开接口，并为旧版免费 TreeDataGrid 的必要字段反射补充裁剪和 NativeAOT 保留声明。
- 🐛[修复]-TreeDataGrid 清除排序时找不到继承层级私有字段不再静默失败。

## 12.1.2.3 / 11.1.1.9 (2026-09-20)

- 🚀[新增]-NuGet 包统一支持 `net8.0;net10.0;net11.0`，并发布新版本。

## 11.1.1.6 (2026-07-30)

- 🐛[修复]-TreeDataGrid 通过覆盖 Semi 模板使用的 `TreeDataGridRowMargin` 和 `TreeDataGridRowCornerRadius` 资源消除行背景缩进，避免低优先级样式无法覆盖模板值。

## 12.1.0.3 / 11.1.1.5 (2026-07-29)

- 🔨[优化]-统一 DataGrid 与 TreeDataGrid 行底边框，普通行使用 `#F0F0F0`，选中行使用 `#D9E3EF`。
- 🐛[修复]-选中背景改为完整铺满整行，避免 Margin 导致单选行视觉上未占满。

## 12.1.0.2 / 11.1.1.4 (2026-07-22)

- 🐛[修复]-DataGrid 与 TreeDataGrid 的智能 ToolTip 文本前景色绑定当前 ToolTip 主题，不再受宿主全局 TextBlock 前景色影响。
- 🔨[优化]-按单元格按需缓存 ToolTip 文本控件，主题切换可实时生效且不增加持续分配。

## 12.0.1.3 / 11.1.1.3 (2026-07-06)

- 😄[新增]-新增 `CodeWF.AvaloniaControls.DataGrid.Themes`，提供 `codewf:DataGridSemiTheme` 入口并复用 `Semi.Avalonia.DataGrid` 主题资源。
- 😄[新增]-新增 `CodeWF.AvaloniaControls.TreeDataGrid.Themes`，同步提供 `codewf:TreeDataGridSemiTheme`，但继续固定在 `Avalonia.Controls.TreeDataGrid 11.1.1` 免费包线。
- 🔨[优化]-`CodeWF.AvaloniaControls.DataGrid` 和 `CodeWF.AvaloniaControls.TreeDataGrid` 核心扩展包移除 Semi 主题包依赖，主题资源改由独立 Themes 包承载。
- 🔨[优化]-示例工程去掉旧后缀命名，改为 `CodeWF.AvaloniaControls.DataGridDemo` 和 `CodeWF.AvaloniaControls.TreeDataGridDemo`。

## 12.0.1.2 / 11.1.1.2 (2026-07-05)

- 😄[新增]-`CodeWF.AvaloniaControls.DataGrid` 新增 `EnableDefaults`、`AddNaturalSorting`、`DataGridNaturalSortComparer` 和 `DataGridEnhancement.UseDefaults`。
- 😄[新增]-`CodeWF.AvaloniaControls.TreeDataGrid` 新增 `EnableDefaults(source)`，用于统一启用三态排序、Ctrl+A 全选和智能 ToolTip。
- 🔨[优化]-示例工程改为调用统一默认增强入口，减少调用方重复组合扩展方法。

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

- Migrated `CodeWF.AvaloniaControls.DataGrid`, `CodeWF.AvaloniaControls.DataGridDemo`, and `CodeWF.AvaloniaControls.TreeDataGridDemo` from the main `CodeWF.AvaloniaControls` repository.
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

## 归档：src\CodeWF.AvaloniaControls.DataGridDemo\UpdateLog.md

# 更新日志

V12.0.2（2026-05-02）

- 😄[新增]-新增当前工程独立更新日志文件，后续 `CodeWF.AvaloniaControls.DataGridDemo` 的变更改为在工程目录内持续记录
- 😄[新增]-新增独立旧版免费 `DataGrid` 专项示例工程，用于承载最后一个免费开源版本链路
- 😄[新增]-新增大数据量 `TabControl` 切换演示场景，便于直观看到旧版 `DataGrid` 在多页签之间切换时的卡顿与重绘压力
- 🔤[优化]-统一补充中文界面文案，并接入 `CodeWF.AvaloniaControls.DataGrid` 扩展方法用于三态排序与智能提示展示

---

## 归档：src\CodeWF.AvaloniaControls.TreeDataGridDemo\UpdateLog.md

# 更新日志

V12.0.2（2026-05-02）

- 😄[新增]-新增当前工程独立更新日志文件，后续 `CodeWF.AvaloniaControls.TreeDataGridDemo` 的变更改为在工程目录内持续记录
- 😄[新增]-新增独立旧版免费 `TreeDataGrid` 专项示例工程，用于承载最后一个免费开源版本链路
- 😄[新增]-新增大数据量 `TabControl` 切换演示场景，便于与旧版 `DataGrid` 做直观的切换流畅度对照
- 🔤[优化]-接入 `CodeWF.AvaloniaControls.DataGrid` 中的 `TreeDataGrid` 扩展方法，并统一整理为中文界面文案
## 2026-06-08 仓库规范整理

- 统一文档维护入口：每个仓库只保留根目录 `README.md` 和根目录 `UpdateLog.md`，清理重复日志、英文文档和语言切换入口。
- 版本维护入口：通用打包元数据保留在根目录 `Directory.Build.props`，包版本由各 NuGet 项目单独维护。
- 不再维护 `global.json`，SDK 选择交给本机或 CI 环境；NuGet 包和应用的目标框架在项目文件中明确声明。
- 统一 NuGet 包文档入口：包 README 统一引用仓库根 `README.md`，更新日志统一引用仓库根 `UpdateLog.md`。
