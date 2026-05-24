# .NET Framework to .NET 10.0 Upgrade Plan

## Overview

**Target**: Random Photo Screensaver Windows Forms application
**Scope**: Single project, ~6.4k LOC, .NET Framework 4.7.2 → .NET 10.0-windows

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: Single-project solution with clear, self-contained scope.

## Tasks

### 01-prerequisites: Verify upgrade prerequisites

Verify that the .NET 10.0 SDK is installed and that any global.json files in the repository are compatible with .NET 10.0. Ensure the development environment is ready for the upgrade.

**Done when**: .NET 10.0 SDK installation confirmed, global.json compatibility verified (or no global.json present), no toolchain blockers identified

---

### 02-sdk-conversion: Convert project to SDK-style format

Convert the RPS 4.csproj from legacy .NET Framework project format to modern SDK-style format while staying on net472. This is a structural change that simplifies the project file and enables modern tooling capabilities. The project will continue targeting .NET Framework 4.7.2 until the next task.

Assessment shows the project uses old-style csproj format with packages.config. SDK-style conversion will automatically migrate to PackageReference format and simplify the project structure.

**Done when**: Project file converted to SDK-style format, builds successfully on net472 with zero errors and zero warnings, all package references migrated to PackageReference format, solution loads in Visual Studio without issues

---

### 03-upgrade-framework: Upgrade to .NET 10.0 and resolve compatibility issues

Update the project's target framework from net472 to net10.0-windows and resolve all resulting compatibility issues. This includes:

- Updating the TargetFramework property to net10.0-windows (Windows Desktop support required for Windows Forms)
- Resolving 2 incompatible packages (System.Data.SQLite 1.0.94.1 → 2.0.3, System.Data.SQLite.Core 1.0.94.0 → 1.0.119)
- Updating Newtonsoft.Json 6.0.5 → 13.0.4 to address security vulnerability CVE-2024-30105
- Fixing 1,181 binary-incompatible API issues (primarily Windows Forms APIs)
- Addressing 92 source-incompatible API issues
- Handling 29 behavioral change scenarios
- Adding System.Drawing.Common NuGet package for GDI+ APIs (71 issues)
- Resolving System.Management NuGet package requirement for WMI APIs (13 issues)
- Migrating legacy data access patterns (LINQ to SQL, 11 issues)
- Updating legacy configuration system references (2 issues)
- Handling AppDomain API replacement (1 issue)

Key technologies requiring migration guidance: Windows Forms (89.9% of issues), System.Drawing/GDI+ (5.5%), WMI/System.Management (1.0%), legacy data access (0.8%).

**Done when**: Project targets net10.0-windows, all packages compatible and updated (including security fixes), solution builds with zero errors and zero warnings, all API compatibility issues resolved, no security vulnerabilities remain

---

### 04-validation: Final solution validation

Run full solution build and execute complete test suite to verify the upgrade is successful. Document any findings or recommendations for post-upgrade improvements.

**Done when**: Solution builds cleanly with zero errors and zero warnings, all tests pass, application launches and basic functionality verified (screensaver displays photos), no runtime errors detected during smoke testing
