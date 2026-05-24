# .NET Version Upgrade: .NET Framework 4.7.2 → .NET 10.0

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: .NET 10.0 (LTS)

## Source Control
- **Source Branch**: master
- **Working Branch**: dotnet-version-upgrade
- **Commit Strategy**: Single Commit at End

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: All-at-Once

### Project Structure
- Project Approach: In-place

### Compatibility
- Unsupported Packages: Resolve Inline (2 incompatible packages)
- Unsupported API Handling: Fix Inline

## Strategy
**Selected**: All-at-Once
**Rationale**: Single-project solution with no inter-project dependencies to manage.

### Execution Constraints
- Single atomic upgrade — all work completed in one pass
- SDK-style conversion and TFM upgrade are separate tasks with different failure modes
- Validate full solution build after upgrade completes
- Fix all warnings during each task — projects should build warning-free
- All package and API compatibility issues resolved inline (no deferred work)
