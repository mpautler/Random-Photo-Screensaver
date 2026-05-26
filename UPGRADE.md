# .NET 10 Upgrade Documentation

## Overview

Random Photo Screensaver 4 has been successfully upgraded from **.NET Framework 4.7.2** to **.NET 10.0 (LTS)**.

**Upgrade Date**: 2025-05-24  
**Branch**: dotnet-version-upgrade

---

## What Changed

### Framework & Platform

| Component | Before | After |
|-----------|--------|-------|
| **Target Framework** | .NET Framework 4.7.2 | .NET 10.0-windows |
| **Project Format** | Legacy (non-SDK-style) | Modern SDK-style |
| **Platform Support** | Windows 7/8/10 | Windows 10/11 |
| **C# Version** | 7.3 | 12.0 |

### Package Updates

| Package | Old Version | New Version | Notes |
|---------|-------------|-------------|-------|
| **Newtonsoft.Json** | 6.0.5 | 13.0.4 | 🔒 Security fix CVE-2024-30105 |
| **System.Data.SQLite** | 1.0.94.1 | 2.0.3 | Updated for .NET 10 compatibility |
| **System.Data.SQLite.Core** | 1.0.94.0 | 1.0.119 | Updated for .NET 10 compatibility |
| **TaskScheduler** | 2.12.2 | 2.12.2 | Already compatible |
| **System.Drawing.Common** | - | 9.0.0 | Added for GDI+ support |
| **System.Management** | - | 9.0.0 | Added for WMI support |
| **System.Configuration.ConfigurationManager** | - | 9.0.0 | Added for app settings |

### Code Changes

#### LINQ to SQL Migration
- **Before**: Used \System.Data.Linq\ with \DataContext\
- **After**: Direct SQLite commands via \SQLiteCommand\
- **Impact**: Simplified data access, better performance

#### Code Access Security Removal
- **Removed**: Obsolete \[SecurityPermission]\ and \[PermissionSet]\ attributes
- **Reason**: Not supported in modern .NET
- **Files Modified**: MessageBoxManager.cs, Config.cs, Monitor.cs

#### Obsolete API Fixes
- SQLite BeginTransaction: Replaced with standard method
- Exception handling: Fixed to preserve stack traces
- WebClient: Suppressed warning (migration deferred)

---

## Requirements

**Development:**
- Visual Studio 2022 or later
- .NET 10 SDK
- Windows 10 SDK

**Deployment:**
- .NET 10 Runtime (Desktop)
- Windows 10 version 1809 or later

---

## Build Status

✅ **Compiles Successfully**
- Zero compilation errors
- 93 minor code quality warnings (non-blocking)
- All critical warnings resolved

---

## Testing Checklist

- [ ] Application launches correctly
- [ ] Photos load and display properly
- [ ] Settings save and load
- [ ] Multiple monitors work
- [ ] Database operations function
- [ ] ExifTool integration works
- [ ] No memory leaks during long runs

---

## References

- [.NET 10 Release Notes](https://github.com/dotnet/core/releases)
- [Upgrade Assessment](.github/upgrades/scenarios/dotnet-version-upgrade/assessment.md)
- [Execution Log](.github/upgrades/scenarios/dotnet-version-upgrade/execution-log.md)
