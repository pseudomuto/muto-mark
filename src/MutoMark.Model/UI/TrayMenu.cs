using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MutoMark.Model
{
    public class TrayMenu : ContextMenuStrip
    {
        public ITrayMenuDataSource DataSource { get; set; }

        public ITrayMenuDelegate Delegate { get; set; }

        public TrayMenu(ITrayMenuDataSource dataSource = null, ITrayMenuDelegate menuDelegate = null)
        {
            this.DataSource = dataSource;
            this.Delegate = menuDelegate;
            this.ReloadData();
        }

        public void ReloadData()
        {
            this.SuspendLayout();

            if (this.DataSource != null)
            {
                this.Items.Clear();
                
                var count = this.DataSource.NumberOfToolStripItems(this);

                for (int i = 0; i < count; i++)
                {
                    var item = this.DataSource.ToolStripItemForIndex(this, i);
                    item.Click += (o, e) =>
                    {
                        if (this.Delegate != null)
                        {
                            this.Delegate.TrayMenuItemClicked(this, item);
                        }
                    };

                    this.Items.Add(item);
                }
            }

            this.ResumeLayout(true);
        }
    }
}
