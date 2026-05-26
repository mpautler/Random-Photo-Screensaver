using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;

namespace RPS
{
    /// <summary>
    /// Compatibility layer to provide HtmlElement-like access to WebView2
    /// This allows gradual migration from WebBrowser to WebView2
    /// </summary>
    public class WebView2HtmlElement
    {
        private readonly WebView2 _webView;
        private readonly string _elementId;

        public WebView2HtmlElement(WebView2 webView, string elementId)
        {
            _webView = webView;
            _elementId = elementId;
        }

        public string GetAttribute(string attributeName)
        {
            var task = GetAttributeAsync(attributeName);
            task.Wait();
            return task.Result;
        }

        public async Task<string> GetAttributeAsync(string attributeName)
        {
            var script = $"document.getElementById({JsonConvert.SerializeObject(_elementId)}).getAttribute({JsonConvert.SerializeObject(attributeName)});";
            var result = await _webView.CoreWebView2.ExecuteScriptAsync(script);
            return result != "null" ? JsonConvert.DeserializeObject<string>(result) : null;
        }

        public void SetAttribute(string attributeName, string value)
        {
            var task = SetAttributeAsync(attributeName, value);
            task.Wait();
        }

        public async Task SetAttributeAsync(string attributeName, string value)
        {
            var script = $"document.getElementById({JsonConvert.SerializeObject(_elementId)}).setAttribute({JsonConvert.SerializeObject(attributeName)}, {JsonConvert.SerializeObject(value)});";
            await _webView.CoreWebView2.ExecuteScriptAsync(script);
        }
    }

    /// <summary>
    /// Compatibility layer to provide HtmlElementCollection-like access to WebView2
    /// </summary>
    public class WebView2HtmlElementCollection : IEnumerable<WebView2HtmlElement>
    {
        private readonly List<WebView2HtmlElement> _elements;

        public WebView2HtmlElementCollection(List<WebView2HtmlElement> elements)
        {
            _elements = elements ?? new List<WebView2HtmlElement>();
        }

        public int Count => _elements.Count;

        public WebView2HtmlElement this[int index] => _elements[index];

        public IEnumerator<WebView2HtmlElement> GetEnumerator() => _elements.GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
