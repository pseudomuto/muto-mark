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

namespace MutoMark.Model.UI
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Visible = false;
            this.trayIcon.Visible = true;

            this.trayMenu.Items[0].PerformClick();
        }
    }
}
