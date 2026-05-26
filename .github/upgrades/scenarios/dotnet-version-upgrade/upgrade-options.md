# Upgrade Options — Random Photo Screensaver

Assessment: 1 Windows Forms project, .NET Framework 4.7.2 → .NET 10.0-windows, 1,308 API issues, 2 incompatible packages

## Strategy

### Upgrade Strategy
Single-project solution — atomic upgrade is the only viable approach.

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Upgrade all projects simultaneously in a single atomic pass. Fastest approach for single-project solutions. |

## Project Structure

### Project Approach
Windows Forms desktop application — in-place upgrade is the standard path for single-project desktop apps.

| Value | Description |
|-------|-------------|
| **In-place** (selected) | Convert project to SDK-style, update TFM to net10.0-windows, resolve all compatibility issues in one pass. |

## Compatibility

### Unsupported Packages
2 incompatible packages detected (System.Data.SQLite libraries) — small count allows inline resolution.

| Value | Description |
|-------|-------------|
| **Resolve Inline** (selected) | Research and resolve each incompatible package within the same task — no deferred work. |
| Defer Resolution | Generate minimal type stubs to make project compile, then create follow-up tasks for real replacements. |
| Compatibility Mode | Keep .NET Framework reference with compatibility shims — may cause runtime failures. |

### Unsupported API Handling
1,181 binary-incompatible APIs detected (mostly Windows Forms) — fix inline is appropriate for this single-project desktop app.

| Value | Description |
|-------|-------------|
| **Fix Inline** (selected) | Resolve every API change within the same task, including complex ones — no stubs to clean up later. |
| Defer Complex Changes | Apply simple replacements inline, generate stubs for complex changes, create resolution subtasks. |
