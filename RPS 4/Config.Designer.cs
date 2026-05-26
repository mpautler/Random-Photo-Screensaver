namespace RPS {
    partial class Config {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.webViewUpdateCheck = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.timerCheckUpdates = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webViewUpdateCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.AllowExternalDrop = false;
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.Location = new System.Drawing.Point(0, 0);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(784, 682);
            this.webView.TabIndex = 0;
            this.webView.ZoomFactor = 1D;
            // 
            // webViewUpdateCheck
            // 
            this.webViewUpdateCheck.AllowExternalDrop = false;
            this.webViewUpdateCheck.CreationProperties = null;
            this.webViewUpdateCheck.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewUpdateCheck.Location = new System.Drawing.Point(0, 0);
            this.webViewUpdateCheck.Name = "webViewUpdateCheck";
            this.webViewUpdateCheck.Size = new System.Drawing.Size(250, 250);
            this.webViewUpdateCheck.TabIndex = 1;
            this.webViewUpdateCheck.Visible = false;
            this.webViewUpdateCheck.ZoomFactor = 1D;
            this.webViewUpdateCheck.NavigationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs>(this.webViewUpdateCheck_NavigationCompleted);
            // 
            // timerCheckUpdates
            // 
            this.timerCheckUpdates.Enabled = true;
            this.timerCheckUpdates.Interval = 10000;
            this.timerCheckUpdates.Tick += new System.EventHandler(this.timerCheckUpdates_Tick);
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 682);
            this.Controls.Add(this.webViewUpdateCheck);
            this.Controls.Add(this.webView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Config";
            this.Text = "Configuration " + AppSettings.Name;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Config_FormClosing);
            this.Load += new System.EventHandler(this.Config_Load);
            this.VisibleChanged += new System.EventHandler(this.Config_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webViewUpdateCheck)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewUpdateCheck;
        private System.Windows.Forms.Timer timerCheckUpdates;
    }
}
