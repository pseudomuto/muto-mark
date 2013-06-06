using MutoMark.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MutoMark.Forms
{
    public partial class SourceWindow : Form
    {
        private string _filePath;
        private MDTransformer _transformer = new MDTransformer();

        delegate void SetDocumentDelegate();

        public SourceWindow(string fileName)
        {
            InitializeComponent();

            this.Text += " - " + fileName;
            this._filePath = fileName;
            this.SetDocument();
            this.SetupWatchDog(fileName);
        }

        private void SetDocument()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDocumentDelegate(this.SetDocument));
            }
            else
            {
                this.fileStatus.Text = "Loading...";
                var markdown = this.GetSource();
                var html = this._transformer.Transform(markdown);
                this.browser.DocumentText = html;
                this.fileStatus.Text = Path.GetFileName(this._filePath);
            }
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
            this.watchDog.Path = Path.GetDirectoryName(fileName);
            this.watchDog.Filter = Path.GetFileName(fileName);

            this.watchDog.Changed += (sender, e) =>
            {
                if (e.ChangeType == WatcherChangeTypes.Changed)
                {
                    lock (this.watchDog.SynchronizingObject)
                    {
                        this.SetDocument();
                    }
                }
            };
        }        
    }
}
