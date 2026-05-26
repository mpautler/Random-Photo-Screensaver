# Error Handling Enhancements

## Overview
Enhanced error handling has been added to catch and log `KeyNotFoundException` and other unhandled exceptions throughout the application.

## Changes Made

### 1. Config.cs - Enhanced getPersistant Methods

**Location**: `RPS 4/Config.cs`

#### getPersistant(string key)
- Added detailed error messages showing which key was not found
- Lists first 10 available keys when a key is missing (helps debugging)
- Logs to Debug output before throwing exception
- Distinguishes between "configuration not loaded" vs "key not found"

#### getPersistantBool(string key)
- Same enhancements as getPersistant
- Better error context for boolean conversion failures

**Example Error Messages**:
```
Configuration not loaded yet. Attempted to access key: 'syncScreens'
Configuration key not found: 'unknownKey'. Available keys: order, transition, ...
```

### 2. RPS.cs - Global Exception Handlers

**Location**: `RPS 4/RPS.cs`

#### Application_ThreadException (New)
Catches UI thread exceptions (Windows Forms events, button clicks, etc.)

**Features**:
- Logs exception type, message, and stack trace
- Logs inner exceptions if present
- Writes to crash log file: `%AppData%\Random Photo Screensaver\crash.log`
- Shows MessageBox when debugger is attached
- Does not interrupt user experience in production

#### CleanUpOnException (Enhanced)
Catches all other unhandled exceptions (background threads, etc.)

**Features**:
- Logs exception type, message, and stack trace
- Logs inner exceptions if present
- Writes to crash log file: `%AppData%\Random Photo Screensaver\crash.log`
- Shows MessageBox when debugging or in config mode
- Properly cleans up FileNodes before exit

#### Main Method (Enhanced)
Added proper exception handler registration:
```csharp
Application.ThreadException += Application_ThreadException;
Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
AppDomain.CurrentDomain.UnhandledException += CleanUpOnException;
```

## Debugging with Enhanced Error Handling

### In Visual Studio
1. Run with `/c` for config mode
2. When `KeyNotFoundException` occurs:
   - Break on exception (if enabled)
   - Check Debug Output window for detailed logs
   - Error message shows:
	 - Which key was accessed
	 - Available keys in configuration
	 - Full stack trace

### Crash Log File
Location: `%AppData%\Random Photo Screensaver\crash.log`

Contains:
- Timestamp of each crash
- Exception type and message
- Full stack trace
- Inner exception details (if any)

**Example Log Entry**:
```
=== 2025-01-15 10:30:45 ===
[ThreadException] KeyNotFoundException: Configuration key not found: 'backgroundColour'. Available keys: order, transition, interval, ...

Stack Trace:
   at RPS.Config.getPersistant(String key) in Config.cs:line 697
   at RPS.Monitor.WebView_NavigationCompleted(...) in Monitor.cs:line 102
   ...
```

## Common KeyNotFoundException Scenarios

### 1. Configuration Not Loaded Yet
**Cause**: Attempting to access config values before `loadPersistantConfig()` completes

**Error**: `Configuration not loaded yet. Attempted to access key: 'syncScreens'`

**Solution**: Ensure config is loaded before accessing persistant values
```csharp
if (this.config.persistantLoaded()) {
	var value = this.config.getPersistant("key");
}
```

### 2. Missing Configuration Key
**Cause**: Accessing a key that doesn't exist in the database

**Error**: `Configuration key not found: 'newFeature'. Available keys: order, transition, ...`

**Solution**: Check if key exists before accessing
```csharp
if (this.config.hasPersistantKey("newFeature")) {
	var value = this.config.getPersistant("newFeature");
} else {
	// Use default value
}
```

### 3. Typo in Key Name
**Cause**: Misspelled configuration key

**Error**: `Configuration key not found: 'backGroundColor'. Available keys: backgroundColor, ...`

**Solution**: Check spelling against available keys in error message

## Testing Error Handling

### To Test Exception Logging
1. Launch in config mode: `RPS4.exe /c`
2. Trigger an error (or artificially throw one)
3. Check:
   - Debug Output window (Visual Studio)
   - Crash log file: `%AppData%\Random Photo Screensaver\crash.log`
   - MessageBox (if debugger attached)

### To Find Configuration Issues
1. Look for KeyNotFoundException in Debug Output
2. Note which key is missing
3. Check available keys listed in error
4. Add proper null checks or default values

## Best Practices

### For Developers
1. Always check `persistantLoaded()` before accessing config
2. Use `hasPersistantKey()` for optional settings
3. Provide default values for missing keys
4. Don't swallow KeyNotFoundException without logging

### For Troubleshooting
1. Enable "Break when exception is thrown" in Visual Studio
2. Check Debug Output for detailed error context
3. Review crash.log for user-reported issues
4. Look for patterns in missing configuration keys

## Future Improvements

### Potential Enhancements
- [ ] Add telemetry for crash reporting (optional, with user consent)
- [ ] Implement configuration validation on startup
- [ ] Add configuration migration for missing keys
- [ ] Create default configuration if database is empty
- [ ] Add more detailed context in error messages (which monitor, which mode, etc.)

## Related Files
- `RPS 4/Config.cs` - Configuration management and getPersistant methods
- `RPS 4/RPS.cs` - Global exception handlers and Main entry point
- `RPS 4/Constants.cs` - Configuration key definitions
- `crash.log` - Runtime error log file
