using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MutoMark.Model
{
    public interface IEditorToolStripDataSource
    {
        int NumberOfEditorToolStripItems(EditorToolStrip instance);

        ToolStripItem EditorToolStripItemForIndex(EditorToolStrip instance, int index);
    }
}
