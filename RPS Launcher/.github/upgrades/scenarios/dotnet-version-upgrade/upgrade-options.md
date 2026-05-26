# Upgrade Options — RPS Launcher.sln

Assessment: 1 Windows Forms project, net472 → net10.0-windows, 0 packages, 125 LOC, low complexity

## Strategy

### Upgrade Strategy
Single project with no dependencies to manage — atomic upgrade is the only viable approach.

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Upgrade all projects simultaneously in a single atomic pass |

## Project Structure

### Project Approach
Windows Forms class library project — in-place upgrade with SDK-style conversion.

| Value | Description |
|-------|-------------|
| **In-place** (selected) | Replace target framework directly, convert to SDK-style project format |

## Modernization

### Nullable Reference Types
Small codebase but nullable warnings would add overhead to the migration — better as a separate post-upgrade effort.

| Value | Description |
|-------|-------------|
| **Leave Disabled** (selected) | Maintain existing null handling, enable nullable as separate effort after migration |
| Enable Nullable Reference Types | Add nullable reference types with compile-time null safety, may require code updates |
