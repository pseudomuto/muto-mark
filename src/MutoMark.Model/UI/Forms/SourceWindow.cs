using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MutoMark.Model
{
    public partial class SourceWindow : Form
    {
        public SourceWindow(string sourceHTML)
        {
            InitializeComponent();

            this.sourceCode.Text = sourceHTML;
        }
    }
}
