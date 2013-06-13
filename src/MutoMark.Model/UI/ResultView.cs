using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MutoMark.Model
{
    public class ResultView : Panel
    {
        private WebBrowser _browser;

        public IResultViewDataSource DataSource { get; set; }

        public string HTML { get; private set; }

        public ResultView()
            : this(null)
        {
        }

        public ResultView(IResultViewDataSource dataSource = null)
        {
            this.DataSource = dataSource;
            this.Reload();
        }

        public void Reload()
        {
            this.SuspendLayout();

            if (this.DataSource != null)
            {
                this.DestroyBrowser();
                this.CreateBrowser();
                this.HTML = this.DataSource.HTMLForResultView(this);
                this._browser.DocumentText = this.HTML;
                
                this.Controls.Add(this._browser);
            }

            this.ResumeLayout(true);
        }

        private void DestroyBrowser()
        {
            if (this._browser != null)
            {
                this.Controls.Remove(this._browser);
                //this._browser.Dispose();
                this._browser = null;
            }
        }

        private void CreateBrowser()
        {
            this._browser = new WebBrowser();
            this._browser.AllowNavigation = false;
            this._browser.IsWebBrowserContextMenuEnabled = false;
            this._browser.Dock = DockStyle.Fill;
        }
    }
}
