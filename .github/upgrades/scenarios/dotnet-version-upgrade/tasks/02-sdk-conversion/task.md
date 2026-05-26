# 02-sdk-conversion: Convert project to SDK-style format

Convert the RPS 4.csproj from legacy .NET Framework project format to modern SDK-style format while staying on net472. This is a structural change that simplifies the project file and enables modern tooling capabilities. The project will continue targeting .NET Framework 4.7.2 until the next task.

Assessment shows the project uses old-style csproj format with packages.config. SDK-style conversion will automatically migrate to PackageReference format and simplify the project structure.

**Done when**: Project file converted to SDK-style format, builds successfully on net472 with zero errors and zero warnings, all package references migrated to PackageReference format, solution loads in Visual Studio without issues
