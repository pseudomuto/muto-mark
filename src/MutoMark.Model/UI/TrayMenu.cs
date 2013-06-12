using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MutoMark.Model.UI
{
    public class TrayMenu : ContextMenuStrip
    {
        private string _rootFolder = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

        public TrayMenu()
        {
            var items = new ToolStripItem[] {
                new ToolStripMenuItem("Open...", null, (o, e) => 
                {
                    using (var dlg = new OpenFileDialog())
                    {
                        dlg.InitialDirectory = this._rootFolder;
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            this._rootFolder = Path.GetDirectoryName(dlg.FileName);
                            new MarkDownView(dlg.FileName).Show();
                        }
                    }
                }),
                new ToolStripSeparator(),
                new ToolStripMenuItem("About", null, (o, e) =>
                {
                }),
                new ToolStripSeparator(),
                new ToolStripMenuItem("Exit", null, (o, e) => 
                { 
                    Application.Exit(); 
                })
            };

            this.SuspendLayout();
            this.Items.AddRange(items);
            this.ResumeLayout(true);
        }
    }
}
