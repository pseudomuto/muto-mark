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

namespace MutoMark.Model
{
    public partial class MainWindow : Form, ITrayMenuDataSource, ITrayMenuDelegate
    {
        private string _recentFolder = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

        private ToolStripItem[] _trayMenuItems = new ToolStripItem[] {
            new ToolStripMenuItem("Open..."),            
            new ToolStripMenuItem("Exit")
        };

        public MainWindow()
        {
            InitializeComponent();
            this.Visible = false;
            
            this.trayIcon.Visible = true;
            this.trayMenu.DataSource = this;
            this.trayMenu.Delegate = this;
            this.trayMenu.ReloadData();

#if DEBUG
            this.OpenFile(@"..\..\..\..\Samples\github.md");
#else
            this.Open();
#endif
        }

        public int NumberOfToolStripItems(TrayMenu instance)
        {
            return this._trayMenuItems.Length;
        }

        public ToolStripItem ToolStripItemForIndex(TrayMenu instance, int index)
        {
            return this._trayMenuItems[index];
        }

        public void TrayMenuItemClicked(TrayMenu instance, ToolStripItem item)
        {            
            switch(item.Text)
            {
                case "Open...":
                    this.Open();
                    break;
                default:
                    Application.Exit();
                    break;
            }
        }

        public void Open()
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.InitialDirectory = this._recentFolder;

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this._recentFolder = Path.GetDirectoryName(dlg.FileName);
                    this.OpenFile(dlg.FileName);
                }
            }
        }

        private void OpenFile(string filePath)
        {
            new ResultWindow(filePath).Show(this);
        }
    }
}
