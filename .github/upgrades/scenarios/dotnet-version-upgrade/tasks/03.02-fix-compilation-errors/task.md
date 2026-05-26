# 03.02-fix-compilation-errors: Fix all compilation errors from framework upgrade

## Objective
Resolve all compilation errors introduced by the framework upgrade, focusing on binary-incompatible APIs and missing references.

## Scope
- Fix Windows Forms API issues (1,181 binary-incompatible issues)
- Fix System.Drawing/GDI+ API issues (71 issues) 
- Fix System.Management/WMI API issues (13 issues)
- Fix LINQ to SQL legacy data access patterns (11 issues)
- Fix configuration system references (2 issues - ApplicationSettingsBase)
- Fix AppDomain API usage (1 issue - GetCurrentThreadId)

## Assessment Context
Key issue files identified:
- MessageBoxManager.cs: AppDomain.GetCurrentThreadId() usage
- ExifToolWrapper.cs: System.Management API usage
- Properties\Settings.Designer.cs: ApplicationSettingsBase
- Wallpaper.cs: System.Drawing.Imaging, Application.ExecutablePath

## Done when
- Project builds with zero compilation errors
- All API compatibility issues resolved
- No obsolete API warnings remain
