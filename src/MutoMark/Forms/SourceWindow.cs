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
        private IMarkdownProcessor _processor = new GitHubProcessor();
        
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
                var doc = new Document(this.GetSource(), this._processor);
                this.browser.DocumentText = doc.ToString();             
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

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }        
    }
}
