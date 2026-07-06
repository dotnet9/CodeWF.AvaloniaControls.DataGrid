@echo off
setlocal enabledelayedexpansion

set "project_paths=src\CodeWF.AvaloniaControls.DataGridDemo src\CodeWF.AvaloniaControls.TreeDataGridDemo"
set "platforms=win-x64 linux-x64"

call "%~dp0publishbase.bat" "%project_paths%" "%platforms%"
