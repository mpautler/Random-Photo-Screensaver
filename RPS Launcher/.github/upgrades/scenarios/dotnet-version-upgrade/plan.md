# .NET 10 Upgrade Plan — RPS Launcher

## Overview

**Target**: RPS Launcher.sln → .NET 10.0 (LTS)
**Scope**: Single Windows Forms project (125 LOC)

### Selected Strategy
**All-at-Once** — Single project upgraded in one atomic operation.
**Rationale**: Single project with no dependencies to manage — atomic upgrade is the only viable approach.

## Tasks

### 01-prerequisites: Verify SDK and tooling compatibility

Validate that the .NET 10 SDK is installed and that any global.json files (if present) are compatible with .NET 10. This ensures the development environment can build the upgraded project.

**Done when**: .NET 10 SDK is confirmed installed, global.json compatibility verified (or no global.json present).

---

### 02-sdk-conversion: Convert project to SDK-style format

Convert RPS Launcher.csproj from classic Windows Forms project format to SDK-style format while remaining on net472. SDK-style projects have cleaner project files, support modern tooling, and are required for .NET 10.

The project currently uses the legacy csproj format with explicit file references and verbose MSBuild syntax. This task converts to SDK-style format which uses implicit globbing and modern project structure.

**Done when**: RPS Launcher.csproj converted to SDK-style format, solution builds successfully on net472 with zero errors.

---

### 03-upgrade-framework: Update target framework to net10.0-windows

Update the target framework from net472 to net10.0-windows. Since the assessment found no packages and no API compatibility issues, this should be a clean TFM update. Fix any compilation errors that arise from framework API changes.

**Done when**: RPS Launcher.csproj targets net10.0-windows, solution builds successfully with zero errors and zero warnings.

---

### 04-validation: Final validation and documentation

Build the full solution, verify zero errors and zero warnings. Document any recommendations for future improvements (e.g., enabling nullable reference types as a separate effort).

**Done when**: Solution builds cleanly, validation complete, any post-upgrade recommendations documented in execution log.
