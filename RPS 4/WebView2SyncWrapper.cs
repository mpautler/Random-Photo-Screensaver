using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;

namespace RPS
{
    /// <summary>
    /// Synchronous wrapper for WebView2 to provide compatibility with legacy WebBrowser code.
    /// This allows minimal code changes during migration from WebBrowser to WebView2.
    /// 
    /// WARNING: Uses .GetAwaiter().GetResult() which blocks the UI thread.
    /// For production code, gradually refactor to use async/await patterns.
    /// </summary>
    public class WebView2SyncWrapper
    {
        private readonly WebView2 _webView;
        private readonly WebView2Helper _helper;
        private bool _isInitialized = false;

        public WebView2SyncWrapper(WebView2 webView)
        {
            _webView = webView ?? throw new ArgumentNullException(nameof(webView));
            _helper = new WebView2Helper(_webView);
        }

        /// <summary>
        /// Initialize WebView2 synchronously (blocks until complete)
        /// </summary>
        public void Initialize(object hostObject = null, string hostObjectName = "config")
        {
            if (_isInitialized) return;

            try
            {
                _helper.InitializeAsync(hostObject, hostObjectName).GetAwaiter().GetResult();
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"WebView2 initialization failed: {ex.Message}", ex);
            }
        }

        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// Navigate to URL synchronously
        /// </summary>
        public void Navigate(string url)
        {
            if (!_isInitialized)
                throw new InvalidOperationException("WebView2 must be initialized before navigation");

            _helper.Navigate(url);
        }

        /// <summary>
        /// Execute JavaScript synchronously
        /// </summary>
        public string ExecuteScript(string script)
        {
            if (!_isInitialized)
                throw new InvalidOperationException("WebView2 must be initialized before executing scripts");

            return _helper.ExecuteScriptAsync(script).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Invoke JavaScript function synchronously (replaces browser.Document.InvokeScript)
        /// </summary>
        public object InvokeScript(string functionName, params string[] args)
        {
            if (!_isInitialized) return null;

            try
            {
                var result = _helper.InvokeScriptAsync(functionName, args).GetAwaiter().GetResult();
                // Try to parse the result
                if (result == "null" || result == "undefined") return null;
                // Return as-is if it's a JSON string or simple value
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"InvokeScript error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Set innerHTML synchronously
        /// </summary>
        public void SetInnerHTML(string elementId, string html)
        {
            if (!_isInitialized) return;
            _helper.SetInnerHTMLAsync(elementId, html).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get innerHTML synchronously
        /// </summary>
        public string GetInnerHTML(string elementId)
        {
            if (!_isInitialized) return null;
            return _helper.GetInnerHTMLAsync(elementId).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Set innerText synchronously
        /// </summary>
        public void SetInnerText(string elementId, string text)
        {
            if (!_isInitialized) return;
            _helper.SetInnerTextAsync(elementId, text).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get element value synchronously
        /// </summary>
        public string GetElementValue(string elementId)
        {
            if (!_isInitialized) return null;
            return _helper.GetElementValueAsync(elementId).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Set element value synchronously
        /// </summary>
        public void SetElementValue(string elementId, string value)
        {
            if (!_isInitialized) return;
            _helper.SetElementValueAsync(elementId, value).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get element attribute synchronously
        /// </summary>
        public string GetElementAttribute(string elementId, string attributeName)
        {
            if (!_isInitialized) return null;
            return _helper.GetElementAttributeAsync(elementId, attributeName).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Set element attribute synchronously
        /// </summary>
        public void SetElementAttribute(string elementId, string attributeName, string value)
        {
            if (!_isInitialized) return;
            _helper.SetElementAttributeAsync(elementId, attributeName, value).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get element by ID synchronously
        /// </summary>
        public WebView2Element GetElementById(string elementId)
        {
            if (!_isInitialized) return null;
            return new WebView2Element(this, elementId);
        }

        /// <summary>
        /// Get elements by tag name synchronously
        /// </summary>
        public WebView2ElementCollection GetElementsByTagName(string tagName)
        {
            if (!_isInitialized) return new WebView2ElementCollection(this, new string[0]);

            // Get all element IDs with this tag name
            var script = $@"
                (function() {{
                    var elements = document.getElementsByTagName({JsonConvert.SerializeObject(tagName)});
                    var ids = [];
                    for (var i = 0; i < elements.length; i++) {{
                        if (!elements[i].id) {{
                            elements[i].id = 'auto_id_' + i + '_' + Date.now();
                        }}
                        ids.push(elements[i].id);
                    }}
                    return ids;
                }})();
            ";

            var result = ExecuteScript(script);
            var ids = JsonConvert.DeserializeObject<string[]>(result);
            return new WebView2ElementCollection(this, ids);
        }

        /// <summary>
        /// Get document HTML synchronously
        /// </summary>
        public string GetDocumentHTML()
        {
            if (!_isInitialized) return null;
            return _helper.GetDocumentHTMLAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get checked state of checkbox/radio synchronously
        /// </summary>
        public bool GetElementChecked(string elementId)
        {
            if (!_isInitialized) return false;
            return _helper.GetElementCheckedAsync(elementId).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get radio button value synchronously
        /// </summary>
        public string GetRadioValue(string containerElementId)
        {
            if (!_isInitialized) return null;
            return _helper.GetRadioValueAsync(containerElementId).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Access to underlying WebView2 control
        /// </summary>
        public WebView2 WebView => _webView;

        /// <summary>
        /// Access to CoreWebView2
        /// </summary>
        public CoreWebView2 CoreWebView2 => _webView?.CoreWebView2;
    }

    /// <summary>
    /// Wrapper to emulate HtmlElement for WebView2
    /// </summary>
    public class WebView2Element
    {
        private readonly WebView2SyncWrapper _wrapper;
        private readonly string _elementId;

        public WebView2Element(WebView2SyncWrapper wrapper, string elementId)
        {
            _wrapper = wrapper;
            _elementId = elementId;
        }

        public string Id => _elementId;

        public string GetAttribute(string attributeName)
        {
            return _wrapper.GetElementAttribute(_elementId, attributeName);
        }

        public void SetAttribute(string attributeName, string value)
        {
            _wrapper.SetElementAttribute(_elementId, attributeName, value);
        }

        public string InnerText
        {
            get => _wrapper.GetElementAttribute(_elementId, "innerText");
            set => _wrapper.SetInnerText(_elementId, value);
        }

        public string InnerHtml
        {
            get => _wrapper.GetInnerHTML(_elementId);
            set => _wrapper.SetInnerHTML(_elementId, value);
        }

        public string OuterHtml => GetAttribute("outerHTML");

        public string TagName => GetAttribute("tagName");
    }

    /// <summary>
    /// Wrapper to emulate HtmlElementCollection for WebView2
    /// </summary>
    public class WebView2ElementCollection : System.Collections.IEnumerable
    {
        private readonly WebView2SyncWrapper _wrapper;
        private readonly string[] _elementIds;

        public WebView2ElementCollection(WebView2SyncWrapper wrapper, string[] elementIds)
        {
            _wrapper = wrapper;
            _elementIds = elementIds ?? new string[0];
        }

        public int Count => _elementIds.Length;

        public WebView2Element this[int index]
        {
            get
            {
                if (index < 0 || index >= _elementIds.Length)
                    throw new IndexOutOfRangeException();
                return new WebView2Element(_wrapper, _elementIds[index]);
            }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            foreach (var id in _elementIds)
            {
                yield return new WebView2Element(_wrapper, id);
            }
        }
    }
}
