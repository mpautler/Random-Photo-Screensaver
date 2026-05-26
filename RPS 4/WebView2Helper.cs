using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;

namespace RPS
{
    /// <summary>
    /// Helper class for WebView2 operations to replace legacy WebBrowser functionality
    /// </summary>
    public class WebView2Helper
    {
        private readonly WebView2 _webView;
        private TaskCompletionSource<bool> _initializationTcs;
        private bool _isInitialized = false;

        public bool IsInitialized => _isInitialized;

        public WebView2Helper(WebView2 webView)
        {
            _webView = webView ?? throw new ArgumentNullException(nameof(webView));
            _initializationTcs = new TaskCompletionSource<bool>();
        }

        /// <summary>
        /// Initialize WebView2 with host object for JavaScript interop
        /// </summary>
        public async Task InitializeAsync(object hostObject, string hostObjectName = "host")
        {
            if (_isInitialized)
                return;

            try
            {
                await _webView.EnsureCoreWebView2Async(null);

                // Add host object for JavaScript to C# communication
                if (hostObject != null)
                {
                    _webView.CoreWebView2.AddHostObjectToScript(hostObjectName, hostObject);
                }

                // Configure WebView2 settings
                _webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                _webView.CoreWebView2.Settings.AreDevToolsEnabled = true; // Enable for debugging
                _webView.CoreWebView2.Settings.IsStatusBarEnabled = false;
                _webView.CoreWebView2.Settings.IsScriptEnabled = true; // Ensure JavaScript is enabled
                _webView.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = true; // Allow alert/confirm dialogs
                _webView.CoreWebView2.Settings.IsWebMessageEnabled = true; // Enable web messaging

                _isInitialized = true;
                _initializationTcs.TrySetResult(true);
            }
            catch (Exception ex)
            {
                _initializationTcs.TrySetException(ex);
                throw;
            }
        }

        /// <summary>
        /// Wait for initialization to complete
        /// </summary>
        public Task WaitForInitializationAsync(int timeoutMs = 10000)
        {
            return Task.WhenAny(_initializationTcs.Task, Task.Delay(timeoutMs)).ContinueWith(t =>
            {
                if (!_initializationTcs.Task.IsCompleted)
                    throw new TimeoutException("WebView2 initialization timed out");
                return _initializationTcs.Task;
            }).Unwrap();
        }

        /// <summary>
        /// Navigate to a URL
        /// </summary>
        public void Navigate(string url)
        {
            if (!_isInitialized)
                throw new InvalidOperationException("WebView2 must be initialized before navigation");

            _webView.CoreWebView2.Navigate(url);
        }

        /// <summary>
        /// Execute JavaScript and return the result
        /// </summary>
        public async Task<string> ExecuteScriptAsync(string script)
        {
            if (!_isInitialized)
                throw new InvalidOperationException("WebView2 must be initialized before executing scripts");

            try
            {
                return await _webView.CoreWebView2.ExecuteScriptAsync(script);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ExecuteScriptAsync error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Invoke a JavaScript function with parameters
        /// </summary>
        public async Task<string> InvokeScriptAsync(string functionName, params string[] args)
        {
            if (!_isInitialized)
                throw new InvalidOperationException("WebView2 must be initialized before invoking scripts");

            // Build JavaScript call
            var jsonArgs = args != null ? string.Join(",", Array.ConvertAll(args, JsonConvert.SerializeObject)) : "";
            var script = $"{functionName}({jsonArgs})";

            return await ExecuteScriptAsync(script);
        }

        /// <summary>
        /// Set innerHTML of an element by ID
        /// </summary>
        public async Task SetInnerHTMLAsync(string elementId, string html)
        {
            var escapedHtml = JsonConvert.SerializeObject(html);
            var script = $"document.getElementById({JsonConvert.SerializeObject(elementId)}).innerHTML = {escapedHtml};";
            await ExecuteScriptAsync(script);
        }

        /// <summary>
        /// Get innerHTML of an element by ID
        /// </summary>
        public async Task<string> GetInnerHTMLAsync(string elementId)
        {
            var script = $"document.getElementById({JsonConvert.SerializeObject(elementId)}).innerHTML;";
            var result = await ExecuteScriptAsync(script);
            return JsonConvert.DeserializeObject<string>(result);
        }

        /// <summary>
        /// Set innerText of an element by ID
        /// </summary>
        public async Task SetInnerTextAsync(string elementId, string text)
        {
            var escapedText = JsonConvert.SerializeObject(text);
            var script = $"document.getElementById({JsonConvert.SerializeObject(elementId)}).innerText = {escapedText};";
            await ExecuteScriptAsync(script);
        }

        /// <summary>
        /// Get value of an input element by ID
        /// </summary>
        public async Task<string> GetElementValueAsync(string elementId)
        {
            var script = $"document.getElementById({JsonConvert.SerializeObject(elementId)}).value;";
            var result = await ExecuteScriptAsync(script);
            return JsonConvert.DeserializeObject<string>(result);
        }

        /// <summary>
        /// Set value of an input element by ID
        /// </summary>
        public async Task SetElementValueAsync(string elementId, string value)
        {
            var escapedValue = JsonConvert.SerializeObject(value);
            var script = $"document.getElementById({JsonConvert.SerializeObject(elementId)}).value = {escapedValue};";
            await ExecuteScriptAsync(script);
        }

        /// <summary>
        /// Set attribute of an element
        /// </summary>
        public async Task SetElementAttributeAsync(string elementId, string attributeName, string value)
        {
            var script = $"document.getElementById({JsonConvert.SerializeObject(elementId)}).setAttribute({JsonConvert.SerializeObject(attributeName)}, {JsonConvert.SerializeObject(value)});";
            await ExecuteScriptAsync(script);
        }

        /// <summary>
        /// Get attribute of an element
        /// </summary>
        public async Task<string> GetElementAttributeAsync(string elementId, string attributeName)
        {
            var script = $"document.getElementById({JsonConvert.SerializeObject(elementId)}).getAttribute({JsonConvert.SerializeObject(attributeName)});";
            var result = await ExecuteScriptAsync(script);
            return result != "null" ? JsonConvert.DeserializeObject<string>(result) : null;
        }

        /// <summary>
        /// Get checked state of a checkbox or radio button
        /// </summary>
        public async Task<bool> GetElementCheckedAsync(string elementId)
        {
            var script = $"document.getElementById({JsonConvert.SerializeObject(elementId)}).checked;";
            var result = await ExecuteScriptAsync(script);
            return result.ToLower() == "true";
        }

        /// <summary>
        /// Get value of a checked radio button in a group
        /// </summary>
        public async Task<string> GetRadioValueAsync(string containerElementId)
        {
            var script = $@"
                (function() {{
                    var container = document.getElementById({JsonConvert.SerializeObject(containerElementId)});
                    if (!container) return null;
                    var radios = container.querySelectorAll('input[type=""radio""]');
                    for (var i = 0; i < radios.length; i++) {{
                        if (radios[i].checked) return radios[i].value;
                    }}
                    return null;
                }})();
            ";
            var result = await ExecuteScriptAsync(script);
            return result != "null" ? JsonConvert.DeserializeObject<string>(result) : null;
        }

        /// <summary>
        /// Get entire document HTML
        /// </summary>
        public async Task<string> GetDocumentHTMLAsync()
        {
            var script = "document.documentElement.outerHTML;";
            var result = await ExecuteScriptAsync(script);
            return JsonConvert.DeserializeObject<string>(result);
        }
    }
}
