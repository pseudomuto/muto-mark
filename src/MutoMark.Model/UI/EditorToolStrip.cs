using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MutoMark.Model
{
    public class EditorToolStrip : ToolStrip
    {
        public IEditorToolStripDataSource DataSource { get; set; }

        public IEditorToolStripDelegate Delegate { get; set; }

        public EditorToolStrip()
            : this(null, null)
        {
        }

        public EditorToolStrip(IEditorToolStripDataSource dataSource, IEditorToolStripDelegate itemDelegate)
        {
            this.DataSource = dataSource;
            this.Delegate = itemDelegate;
            this.Reload();
        }

        public void Reload()
        {
            if (this.DataSource != null)
            {
                this.SuspendLayout();

                this.Items.Clear();

                var count = this.DataSource.NumberOfEditorToolStripItems(this);
                for (int i = 0; i < count; i++)
                {
                    var item = this.DataSource.EditorToolStripItemForIndex(this, i);

                    item.Click += (sender, e) =>
                    {
                        if (this.Delegate != null)
                        {
                            this.Delegate.EditorToolStripItemClicked(this, item);
                        }
                    };

                    if (item is ToolStripDropDownButton)
                    {
                        (item as ToolStripDropDownButton).DropDownItemClicked += (sender, e) =>
                        {
                            if (this.Delegate != null)
                            {
                                this.Delegate.EditorToolStripItemClicked(this, e.ClickedItem);
                            }
                        };
                    }

                    this.Items.Add(item);
                }

                this.ResumeLayout(true);
            }
        }
    }
}
