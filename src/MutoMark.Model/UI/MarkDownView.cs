using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MutoMark.Model
{
    public partial class MarkDownView : Form, IObserver<Document>
    {
        private string _filePath;
        private IMarkdownProcessor _processor = new DefaultProcessor();

        private WatchDog _watchDog;
        private IDisposable _unsubscriber;

        delegate void SetDocumentDelegate(Document markDownDocument);

        public MarkDownView(string fileName)
        {
            InitializeComponent();
            
            this.Text += " - " + fileName;
            this._filePath = fileName;

            this.SetupWatchDog(fileName);
            this.styleButton.DropDownItems[1].PerformClick();
        }

        private void SetDocument(Document source)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDocumentDelegate(this.SetDocument), source);
            }
            else
            {
                this.browser.DocumentText = source.ToString();
            }
        }

        private void SetDocument()
        {
            var doc = new Document(this.GetSource(), this._processor);
            this.SetDocument(doc);
        }

        private string GetSource()
        {
            using (var fs = new FileStream(this._filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(fs))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private void SetupWatchDog(string fileName)
        {
            this._watchDog = new WatchDog(fileName, this._processor);
            this._unsubscriber = this._watchDog.Subscribe(this);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this._unsubscriber.Dispose();
            this._watchDog.Dispose();
        }

        #region [IObserver<Document> Implementation]
        
        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(Document value)
        {
            this.SetDocument(value);
        }

        #endregion                

        private void StyleSelectionChanged(object sender, EventArgs e)
        {
            var item = sender as ToolStripItem;
            if (item != null)
            {
                this.styleButton.Text = string.Concat("Style: ", item.Text);

                switch(item.Text.ToLower())
                {
                    case "github":
                        this._processor = new GitHubProcessor();
                        break;
                    default:
                        this._processor = new DefaultProcessor();
                        break;
                }

                this._watchDog.Processor = this._processor;
                this.SetDocument();
            }
        }
    }
}
