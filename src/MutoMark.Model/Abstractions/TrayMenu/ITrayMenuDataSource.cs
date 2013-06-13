using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MutoMark.Model
{
    public interface ITrayMenuDataSource
    {
        int NumberOfToolStripItems(TrayMenu instance);

        ToolStripItem ToolStripItemForIndex(TrayMenu instance, int index);
    }
}
