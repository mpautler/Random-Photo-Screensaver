# Task 03: TFM Upgrade to .NET 10.0

## Summary
Upgraded RPS Launcher project from net472 to net10.0-windows. Updated post-build event to handle .NET 10 runtime file structure. Removed redundant assembly references. Suppressed appropriate platform-specific warnings.

## Changes Made
- Updated TargetFramework from net472 to net10.0-windows
- Removed redundant assembly references (System.Data.DataSetExtensions, Microsoft.CSharp)
- Updated post-build event to handle .NET 10 file structure (dll.config, runtimeconfig.json)
- Added NoWarn for CA1416 (appropriate for Windows-only application)

## Files Modified
- RPS Launcher\RPS Launcher\RPS Launcher.csproj

## Validation
- ? Project builds successfully on net10.0-windows
- ? Zero errors
- ? Zero warnings
- ? Post-build event executes successfully

## Status
TFM upgrade complete. Project ready for final validation.
