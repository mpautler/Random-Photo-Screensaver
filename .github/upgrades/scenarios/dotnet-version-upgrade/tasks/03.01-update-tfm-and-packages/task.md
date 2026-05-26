# 03.01-update-tfm-and-packages: Update target framework and package versions

## Objective
Update project target framework from net472 to net10.0-windows and update all package versions to compatible versions.

## Scope
- Update TargetFramework property to net10.0-windows
- Update Newtonsoft.Json 6.0.5 → 13.0.4 (security fix CVE-2024-30105)
- Update System.Data.SQLite 1.0.94.1 → 2.0.3
- Update System.Data.SQLite.Core 1.0.94.0 → 1.0.119
- Update TaskScheduler to latest compatible version
- Add System.Drawing.Common package (required for GDI+ APIs)
- Add System.Management package (required for WMI APIs)
- Add System.Configuration.ConfigurationManager package (required for ApplicationSettingsBase)

## Done when
- Project targets net10.0-windows
- All packages updated to compatible versions
- Project restores successfully (packages downloaded)
