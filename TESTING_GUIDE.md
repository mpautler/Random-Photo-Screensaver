# Testing the RPS WebView2 Migration

## Quick Start - Config Mode Testing

### Option 1: Visual Studio (Recommended)

1. **Set Command-Line Arguments**:
   - Right-click on `RPS 4` project → **Properties**
   - Go to **Debug** → **General**
   - Find **Command line arguments** and enter: `/c`
   - Click **Save**

2. **Run the Project**:
   - Press **F5** (Debug) or **Ctrl+F5** (Run without debugging)
   - The configuration window should open

### Option 2: Command Line

1. **Build the project**:
   ```powershell
   dotnet build "RPS 4\RPS 4.csproj" --configuration Debug
   ```

2. **Run in Config mode**:
   ```powershell
   & "RPS 4\bin\Debug\net10.0-windows\RPS4.exe" /c
   ```

### Option 3: PowerShell Script

Create a test script `test-config.ps1`:
```powershell
# Build the project
dotnet build "RPS 4\RPS 4.csproj" --configuration Debug

if ($LASTEXITCODE -eq 0) {
	# Run in config mode
	& "RPS 4\bin\Debug\net10.0-windows\RPS4.exe" /c
} else {
	Write-Error "Build failed"
}
```

Run it:
```powershell
.\test-config.ps1
```

## Command-Line Arguments Reference

Based on the code in RPS.cs, here are all available modes:

| Argument | Mode | Description |
|----------|------|-------------|
| `/c` | Config | Opens the configuration window (what you want) |
| `/s` | Screensaver | Runs as fullscreen screensaver |
| `/p <hwnd>` | Preview | Preview mode in Windows screensaver settings |
| `/t <hwnd>` | Test | Test mode |
| `/w` | Wallpaper | Generate wallpaper |
| `/a <path>` | Admin | Set as current screensaver |
| `/o on/off` | Toggle | Turn screensaver on/off |
| `/x <path>` | Set Wallpaper | Set specific wallpaper |

## Important: WebView2 Runtime Required

Before testing, ensure WebView2 Runtime is installed:

### Check if WebView2 Runtime is installed:
```powershell
# Check for WebView2 Runtime
$webview2Path = "${env:ProgramFiles(x86)}\Microsoft\EdgeWebView\Application"
if (Test-Path $webview2Path) {
	Write-Host "✓ WebView2 Runtime is installed" -ForegroundColor Green
	Get-ChildItem $webview2Path -Directory | Select-Object Name
} else {
	Write-Host "✗ WebView2 Runtime NOT found" -ForegroundColor Red
	Write-Host "Download from: https://developer.microsoft.com/microsoft-edge/webview2/"
}
```

### Install WebView2 Runtime (if needed):
1. **Download**: https://developer.microsoft.com/microsoft-edge/webview2/
2. **Select**: "Evergreen Bootstrapper" or "Evergreen Standalone Installer"
3. **Install** and restart your IDE

## What to Test in Config Mode

### Basic Functionality
- [ ] Config window opens
- [ ] WebView2 renders config.html correctly
- [ ] All tabs are visible and clickable
- [ ] No error messages in the title bar

### JavaScript Bridge (C# ↔ JavaScript)
- [ ] Try changing a setting (e.g., folder selection)
- [ ] Click "Add Folder" button (tests JavaScript → C# call)
- [ ] Save settings (tests data persistence)
- [ ] Check if settings load on restart

### Known Limitations (Phase 3 not complete)
- ⚠️ Monitor/Preview mode not yet migrated (still uses WebBrowser)
- ⚠️ Screensaver mode may have issues (Monitor.cs not migrated)
- ✅ Config mode should work fully

## Troubleshooting

### Error: "WebView2 Runtime not found"
**Solution**: Install WebView2 Runtime (see above)

### Error: "Failed to initialize WebView2"
**Possible causes**:
1. WebView2 Runtime not installed
2. Corrupted WebView2 installation
3. Missing user data folder permissions

**Solution**:
```powershell
# Reinstall WebView2 Runtime
# Or specify a custom user data folder in Config_Load:
# await webView.EnsureCoreWebView2Async(new CoreWebView2Environment.CreateAsync(...))
```

### Config window is blank
**Possible causes**:
1. HTML file not found (check data folder path)
2. Navigation failed
3. JavaScript errors

**Debug steps**:
1. Enable DevTools: In `Config_Load`, ensure:
   ```csharp
   _webView.CoreWebView2.Settings.AreDevToolsEnabled = true;
   ```
2. Right-click in the blank window and select "Inspect" (if DevTools enabled)
3. Check Console for JavaScript errors

### No JavaScript bridge (buttons don't work)
**Cause**: Host object not registered properly

**Check**: Look for this in Config_Load:
```csharp
browser.Initialize(this, "config");  // "config" is the host object name
```

**JavaScript should use**:
```javascript
window.chrome.webview.hostObjects.config.methodName()
```

## Debugging Tips

### Enable WebView2 DevTools
Add to Config_Load after initialization:
```csharp
browser.WebView.CoreWebView2.Settings.AreDevToolsEnabled = true;
```

### View Debug Output
Run in Debug mode (F5) and check the Output window:
- View → Output
- Show output from: Debug

### Check Initialization
Add breakpoints in:
- `Config_Load` (RPS 4\Config.cs, line ~86)
- `WebView_NavigationCompleted` (RPS 4\Config.cs, line ~821)

## Expected Behavior

When you run `/c`, you should see:

1. **Splash/Loading**: Brief initialization
2. **Config Window Opens**: ~800x700 window titled "Configuration Random Photo Screensaver™"
3. **HTML Renders**: WebView2 shows the config interface
4. **Navigation Bar**: Tabs: Folders, Slideshow, Filters, Media, Metadata, etc.
5. **Version Display**: Shows "RPS 4.x" (without IE version - that's been removed)

## Next Steps After Testing

If config mode works:
1. ✅ **Phase 2 Complete** - Config.cs migration successful
2. 🚧 **Start Phase 3** - Migrate Monitor.cs using same patterns
3. 🧪 **Test Preview Mode** - After Monitor.cs migration
4. 🧪 **Test Screensaver Mode** - Full integration test

## Quick Test Checklist

```
Config Mode Test:
□ Application starts without errors
□ WebView2 initializes successfully
□ Config window displays correctly
□ HTML content renders
□ Tabs are clickable
□ "Add Folder" button works (tests JS→C# bridge)
□ Settings save and reload
□ Version shows "RPS 4.x" (no IE version)
□ No console errors (if DevTools enabled)
```

---

**Good luck with testing!** If you encounter issues, check the Output window in Visual Studio for debug messages, or enable DevTools to inspect the WebView2 content.
