using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace RPS
{
    /// <summary>
    /// COM-visible host object for WebView2 JavaScript interop.
    /// This class exposes methods that can be called from JavaScript via window.chrome.webview.hostObjects.config
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class ConfigHostObject
    {
        private readonly Config _config;

        public ConfigHostObject(Config config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void Message(string text)
        {
            MessageBox.Show(text);
        }

        public string jsFileBrowserDialog(string filename, string filter)
        {
            return _config.jsFileBrowserDialog(filename, filter);
        }

        public string jsFolderBrowserDialog(string path)
        {
            return _config.jsFolderBrowserDialog(path);
        }

        public string jsRawConverterAvailable(string path)
        {
            return _config.jsRawConverterAvailable(path);
        }

        public string jsGetUFRawLocation()
        {
            return _config.jsGetUFRawLocation();
        }

        public void jsInputChanged(string id, string value)
        {
            _config.jsInputChanged(id, value);
        }

        public void jsSetSelectedEffects(string jsonEffects)
        {
            _config.jsSetSelectedEffects(jsonEffects);
        }

        public void jsOpenExternalLink(string href)
        {
            _config.jsOpenExternalLink(href);
        }

        public string jsGetSelectedEffects()
        {
            return _config.jsGetSelectedEffects();
        }

        public string jsGetFilters()
        {
            return _config.jsGetFilters();
        }

        public string jsGetFilterColumns()
        {
            return _config.jsGetFilterColumns();
        }

        public void jsOpenProgramAppDataFolder()
        {
            _config.jsOpenProgramAppDataFolder();
        }

        public string jsonAllPersistant()
        {
            return _config.jsonAllPersistant();
        }

        public void jsApplyFilter(string filter)
        {
            _config.jsApplyFilter(filter);
        }

        public void jsClearFilter(string jsDummy)
        {
            _config.jsClearFilter(jsDummy);
        }

        public bool jsSetGPURendering()
        {
            return _config.jsSetGPURendering();
        }

        public string getInitialFoldersJSON(bool dumdum)
        {
            return _config.getInitialFoldersJSON(dumdum);
        }
    }
}
