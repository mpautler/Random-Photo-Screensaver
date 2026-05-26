# .NET Version Upgrade - RPS Launcher.sln

## Strategy
**Selected**: All-at-Once
**Rationale**: Single-project solution with no inter-project dependencies to manage.

### Execution Constraints
- Single atomic upgrade — all work completed in one pass
- SDK-style conversion and TFM upgrade are separate tasks with different failure modes
- Validate full solution build after upgrade completes
- Fix all warnings during each task — projects should build warning-free

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: .NET 10.0 (LTS)
- **Solution**: RPS Launcher.sln

## Upgrade Options

### Strategy
- **Upgrade Strategy**: All-at-Once

### Project Structure
- **Project Approach**: In-place

### Modernization
- **Nullable Reference Types**: Leave Disabled

## Source Control
- **Source Branch**: dotnet-version-upgrade
- **Working Branch**: dotnet-version-upgrade-launcher
- **Commit Strategy**: Single Commit at End
