# CodeWF.AvaloniaControls.DataGrid

| Name | NuGet | Download |
|------|-------|----------|
| CodeWF.AvaloniaControls.DataGrid | [![NuGet](https://img.shields.io/nuget/v/CodeWF.AvaloniaControls.DataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.DataGrid/) | [![NuGet](https://img.shields.io/nuget/dt/CodeWF.AvaloniaControls.DataGrid.svg)](https://www.nuget.org/packages/CodeWF.AvaloniaControls.DataGrid/) |

Legacy free Avalonia DataGrid / TreeDataGrid helper package and runnable samples.

English | [简体中文](README.zh-CN.md)

## Install

```shell
Install-Package CodeWF.AvaloniaControls.DataGrid
```

## Package Scope

`CodeWF.AvaloniaControls.DataGrid` stays on the last free official Avalonia grid package line:

- `Avalonia.Controls.DataGrid` `11.3.7`
- `Avalonia.Controls.TreeDataGrid` `11.1.1`
- `Semi.Avalonia.DataGrid` `11.3.7.3`
- `Semi.Avalonia.TreeDataGrid` `11.1.1.1`

The package provides extension methods for tri-state sorting, smart DataGrid tooltips, TreeDataGrid tri-state sorting, and TreeDataGrid select-all behavior.

## Repository Layout

- `src/CodeWF.AvaloniaControls.DataGrid`: NuGet library
- `src/CodeWF.AvaloniaControls.DataGridLegacyDemo`: legacy DataGrid stress sample
- `src/CodeWF.AvaloniaControls.TreeDataGridLegacyDemo`: legacy TreeDataGrid stress sample
- `CodeWF.AvaloniaControls.DataGrid.slnx`: standalone solution

## Scripts

- `pack.bat`: restore, build, and pack the DataGrid library into `artifacts/packages`
- `publish_all.bat`: publish the DataGrid and TreeDataGrid demo applications into `publish/`
- `publishbase.bat`: shared publish helper used by `publish_all.bat`

## Notes

This repository is intentionally isolated from the Avalonia 12 main-line controls repository so the legacy free grid packages can keep explicit package versions without adding compatibility branches to the main repository.
