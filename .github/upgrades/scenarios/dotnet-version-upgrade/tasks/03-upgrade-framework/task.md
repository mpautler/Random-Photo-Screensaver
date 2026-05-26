# 03-upgrade-framework: Upgrade to .NET 10.0 and resolve compatibility issues

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
