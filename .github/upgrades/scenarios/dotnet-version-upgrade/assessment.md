# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v10.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [RPS 4\RPS 4.csproj](#rps-4rps-4csproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 1 | All require upgrade |
| Total NuGet Packages | 3 | All packages need upgrade |
| Total Code Files | 21 |  |
| Total Code Files with Incidents | 15 |  |
| Total Lines of Code | 6434 |  |
| Total Number of Issues | 1308 |  |
| Estimated LOC to modify | 1302+ | at least 20.2% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [RPS 4\RPS 4.csproj](#rps-4rps-4csproj) | net472 | 🟡 Medium | 4 | 1302 | 1302+ | ClassicWinForms, Sdk Style = False |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 0 | 0.0% |
| ⚠️ Incompatible | 2 | 66.7% |
| 🔄 Upgrade Recommended | 1 | 33.3% |
| ***Total NuGet Packages*** | ***3*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 1181 | High - Require code changes |
| 🟡 Source Incompatible | 92 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 29 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 11886 |  |
| ***Total APIs Analyzed*** | ***13188*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| Newtonsoft.Json | 6.0.5 | 13.0.4 | [RPS 4.csproj](#rps-4rps-4csproj) | NuGet package upgrade is recommended |
| System.Data.SQLite | 1.0.94.1 | 2.0.3 | [RPS 4.csproj](#rps-4rps-4csproj) | ⚠️NuGet package is incompatible |
| System.Data.SQLite.Core | 1.0.94.0 | 1.0.119 | [RPS 4.csproj](#rps-4rps-4csproj) | ⚠️NuGet package is incompatible |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| Windows Forms | 1170 | 89.9% | Windows Forms APIs for building Windows desktop applications with traditional Forms-based UI that are available in .NET on Windows. Enable Windows Desktop support: Option 1 (Recommended): Target net9.0-windows; Option 2: Add <UseWindowsDesktop>true</UseWindowsDesktop>; Option 3 (Legacy): Use Microsoft.NET.Sdk.WindowsDesktop SDK. |
| GDI+ / System.Drawing | 71 | 5.5% | System.Drawing APIs for 2D graphics, imaging, and printing that are available via NuGet package System.Drawing.Common. Note: Not recommended for server scenarios due to Windows dependencies; consider cross-platform alternatives like SkiaSharp or ImageSharp for new code. |
| System Management (WMI) | 13 | 1.0% | Windows Management Instrumentation (WMI) APIs for system administration and monitoring that are available via NuGet package System.Management. These APIs provide access to Windows system information but are Windows-only; consider cross-platform alternatives for new code. |
| Legacy Data Access | 11 | 0.8% | LINQ to SQL and deprecated ADO.NET components that have been superseded by more modern data access technologies. LINQ to SQL was an early ORM that has been replaced by Entity Framework. Migrate to Entity Framework Core or modern ADO.NET. |
| Legacy Configuration System | 2 | 0.2% | Legacy XML-based configuration system (app.config/web.config) that has been replaced by a more flexible configuration model in .NET Core. The old system was rigid and XML-based. Migrate to Microsoft.Extensions.Configuration with JSON/environment variables; use System.Configuration.ConfigurationManager NuGet package as interim bridge if needed. |
| AppDomain APIs | 1 | 0.1% | AppDomain-related APIs that are unsupported in .NET Core due to architectural changes. AppDomains were used for isolation but have been replaced by AssemblyLoadContext API for loading assemblies. Most AppDomain scenarios are not supported; redesign application isolation approach. |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |
| T:System.Windows.Forms.Keys | 123 | 9.4% | Binary Incompatible |
| T:System.Windows.Forms.WebBrowser | 112 | 8.6% | Binary Incompatible |
| T:System.Windows.Forms.Timer | 52 | 4.0% | Binary Incompatible |
| M:System.Windows.Forms.HtmlElement.GetAttribute(System.String) | 49 | 3.8% | Binary Incompatible |
| T:System.Windows.Forms.HtmlDocument | 40 | 3.1% | Binary Incompatible |
| P:System.Windows.Forms.WebBrowser.Document | 40 | 3.1% | Binary Incompatible |
| P:System.Windows.Forms.Control.Bounds | 35 | 2.7% | Binary Incompatible |
| T:System.Windows.Forms.DialogResult | 25 | 1.9% | Binary Incompatible |
| T:System.Windows.Forms.Application | 23 | 1.8% | Binary Incompatible |
| T:System.Windows.Forms.PreviewKeyDownEventHandler | 22 | 1.7% | Binary Incompatible |
| T:System.Windows.Forms.Screen | 20 | 1.5% | Binary Incompatible |
| P:System.Windows.Forms.Screen.AllScreens | 20 | 1.5% | Binary Incompatible |
| T:System.Windows.Forms.HtmlElement | 20 | 1.5% | Binary Incompatible |
| T:System.Uri | 19 | 1.5% | Behavioral Change |
| T:System.Windows.Forms.MessageBoxIcon | 16 | 1.2% | Binary Incompatible |
| T:System.Windows.Forms.MessageBoxButtons | 16 | 1.2% | Binary Incompatible |
| P:System.Windows.Forms.Timer.Interval | 16 | 1.2% | Binary Incompatible |
| T:System.Windows.Forms.FormWindowState | 15 | 1.2% | Binary Incompatible |
| M:System.Windows.Forms.HtmlDocument.GetElementById(System.String) | 12 | 0.9% | Binary Incompatible |
| M:System.Windows.Forms.HtmlElement.SetAttribute(System.String,System.String) | 12 | 0.9% | Binary Incompatible |
| T:System.Windows.Forms.Cursor | 11 | 0.8% | Binary Incompatible |
| T:System.Windows.Forms.MessageBox | 11 | 0.8% | Binary Incompatible |
| E:System.Windows.Forms.Control.PreviewKeyDown | 11 | 0.8% | Binary Incompatible |
| M:System.Windows.Forms.Application.Exit | 10 | 0.8% | Binary Incompatible |
| P:System.Windows.Forms.Timer.Enabled | 10 | 0.8% | Binary Incompatible |
| T:System.Windows.Forms.WebBrowserDocumentCompletedEventHandler | 10 | 0.8% | Binary Incompatible |
| T:System.Windows.Forms.HtmlElementCollection | 10 | 0.8% | Binary Incompatible |
| M:System.Windows.Forms.HtmlDocument.InvokeScript(System.String,System.Object[]) | 10 | 0.8% | Binary Incompatible |
| P:System.Windows.Forms.PreviewKeyDownEventArgs.Control | 9 | 0.7% | Binary Incompatible |
| P:System.Windows.Forms.WebBrowser.Url | 9 | 0.7% | Binary Incompatible |
| M:System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon) | 8 | 0.6% | Binary Incompatible |
| P:System.Windows.Forms.Screen.Bounds | 8 | 0.6% | Binary Incompatible |
| M:System.Windows.Forms.Timer.Stop | 7 | 0.5% | Binary Incompatible |
| P:System.Windows.Forms.Control.Visible | 7 | 0.5% | Binary Incompatible |
| M:System.Windows.Forms.HtmlDocument.GetElementsByTagName(System.String) | 7 | 0.5% | Binary Incompatible |
| M:System.Windows.Forms.Cursor.Show | 6 | 0.5% | Binary Incompatible |
| T:System.Drawing.GraphicsUnit | 6 | 0.5% | Source Incompatible |
| P:System.Windows.Forms.MouseEventArgs.Y | 6 | 0.5% | Binary Incompatible |
| P:System.Windows.Forms.MouseEventArgs.X | 6 | 0.5% | Binary Incompatible |
| M:System.Windows.Forms.HtmlDocument.InvokeScript(System.String) | 6 | 0.5% | Binary Incompatible |
| M:System.Windows.Forms.Control.Show | 6 | 0.5% | Binary Incompatible |
| P:System.Windows.Forms.Control.Name | 6 | 0.5% | Binary Incompatible |
| T:System.Windows.Forms.AutoScaleMode | 6 | 0.5% | Binary Incompatible |
| T:System.Windows.Forms.DockStyle | 6 | 0.5% | Binary Incompatible |
| P:System.Windows.Forms.Control.Handle | 6 | 0.5% | Binary Incompatible |
| P:System.Management.ManagementBaseObject.Item(System.String) | 5 | 0.4% | Source Incompatible |
| M:System.Windows.Forms.Cursor.Hide | 5 | 0.4% | Binary Incompatible |
| F:System.Windows.Forms.MessageBoxButtons.OK | 5 | 0.4% | Binary Incompatible |
| T:System.Windows.Forms.MouseButtons | 5 | 0.4% | Binary Incompatible |
| P:System.Windows.Forms.PreviewKeyDownEventArgs.KeyCode | 5 | 0.4% | Binary Incompatible |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>⚙️&nbsp;RPS 4.csproj</b><br/><small>net472</small>"]
    click P1 "#rps-4rps-4csproj"

```

## Project Details

<a id="rps-4rps-4csproj"></a>
### RPS 4\RPS 4.csproj

#### Project Info

- **Current Target Framework:** net472
- **Proposed Target Framework:** net10.0-windows
- **SDK-style**: False
- **Project Kind:** ClassicWinForms
- **Dependencies**: 0
- **Dependants**: 0
- **Number of Files**: 27
- **Number of Files with Incidents**: 15
- **Lines of Code**: 6434
- **Estimated LOC to modify**: 1302+ (at least 20.2% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["RPS 4.csproj"]
        MAIN["<b>⚙️&nbsp;RPS 4.csproj</b><br/><small>net472</small>"]
        click MAIN "#rps-4rps-4csproj"
    end

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 1181 | High - Require code changes |
| 🟡 Source Incompatible | 92 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 29 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 11886 |  |
| ***Total APIs Analyzed*** | ***13188*** |  |

#### Project Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| AppDomain APIs | 1 | 0.1% | AppDomain-related APIs that are unsupported in .NET Core due to architectural changes. AppDomains were used for isolation but have been replaced by AssemblyLoadContext API for loading assemblies. Most AppDomain scenarios are not supported; redesign application isolation approach. |
| Legacy Configuration System | 2 | 0.2% | Legacy XML-based configuration system (app.config/web.config) that has been replaced by a more flexible configuration model in .NET Core. The old system was rigid and XML-based. Migrate to Microsoft.Extensions.Configuration with JSON/environment variables; use System.Configuration.ConfigurationManager NuGet package as interim bridge if needed. |
| System Management (WMI) | 13 | 1.0% | Windows Management Instrumentation (WMI) APIs for system administration and monitoring that are available via NuGet package System.Management. These APIs provide access to Windows system information but are Windows-only; consider cross-platform alternatives for new code. |
| Legacy Data Access | 11 | 0.8% | LINQ to SQL and deprecated ADO.NET components that have been superseded by more modern data access technologies. LINQ to SQL was an early ORM that has been replaced by Entity Framework. Migrate to Entity Framework Core or modern ADO.NET. |
| GDI+ / System.Drawing | 71 | 5.5% | System.Drawing APIs for 2D graphics, imaging, and printing that are available via NuGet package System.Drawing.Common. Note: Not recommended for server scenarios due to Windows dependencies; consider cross-platform alternatives like SkiaSharp or ImageSharp for new code. |
| Windows Forms | 1170 | 89.9% | Windows Forms APIs for building Windows desktop applications with traditional Forms-based UI that are available in .NET on Windows. Enable Windows Desktop support: Option 1 (Recommended): Target net9.0-windows; Option 2: Add <UseWindowsDesktop>true</UseWindowsDesktop>; Option 3 (Legacy): Use Microsoft.NET.Sdk.WindowsDesktop SDK. |

