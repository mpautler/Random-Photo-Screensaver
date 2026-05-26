# 02-sdk-conversion: Convert project to SDK-style format

Convert the RPS 4.csproj from legacy .NET Framework project format to modern SDK-style format while staying on net472. This is a structural change that simplifies the project file and enables modern tooling capabilities. The project will continue targeting .NET Framework 4.7.2 until the next task.

Assessment shows the project uses old-style csproj format with packages.config. SDK-style conversion will automatically migrate to PackageReference format and simplify the project structure.

## Research Findings

**Project**: RPS 4\RPS 4.csproj
- Current format: Legacy non-SDK-style
- Target framework: net472 (unchanged during conversion)
- Package management: packages.config → PackageReference migration required

**Conversion Issues Encountered**:
1. Invalid package reference `v4` created by conversion tool (removed)
2. Missing TaskScheduler package reference:
   - Old project had local DLL reference: `..\packages\TaskScheduler\v4.0\Microsoft.Win32.TaskScheduler.dll`
   - Not in packages.config (was a manual reference)
   - Added as proper NuGet package: `TaskScheduler` version 2.12.2

**Files Modified**:
- RPS 4\RPS 4.csproj: Converted to SDK-style format
- RPS 4\packages.config: Removed (migrated to PackageReference)

**Done when**: Project file converted to SDK-style format, builds successfully on net472 with zero errors and zero warnings, all package references migrated to PackageReference format, solution loads in Visual Studio without issues
