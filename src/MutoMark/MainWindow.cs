using MutoMark.Forms;
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

namespace MutoMark
{
    public partial class MainWindow : Form
    {
        private string _rootFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.InitialDirectory = this._rootFolder;

                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    this._rootFolder = Path.GetDirectoryName(dlg.FileName);

                    var frm = new SourceWindow(dlg.FileName);
                    frm.Show();
                }
            }
        }
    }
}
