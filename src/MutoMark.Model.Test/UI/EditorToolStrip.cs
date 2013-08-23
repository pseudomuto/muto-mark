using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xunit;

using Should.Fluent;

namespace MutoMark.Model.Test.UI
{
    public abstract class EditorToolStrip : IDisposable
    {
        protected Model.EditorToolStrip _subject;
        private Mock<IEditorToolStripDataSource> _dataSourceMock;

        public EditorToolStrip()
        {
            var dropDown = new ToolStripDropDownButton();
            dropDown.Text = "Style";
            dropDown.Alignment = ToolStripItemAlignment.Right;
            dropDown.DropDownItems.Add("Default");
            dropDown.DropDownItems.Add("GitHub");

            var items = new ToolStripItem[] {
                dropDown,
                new ToolStripMenuItem("Some Button")
            };

            this._dataSourceMock = new Mock<IEditorToolStripDataSource>();
            this._dataSourceMock.Setup(m => m.NumberOfEditorToolStripItems(It.IsAny<Model.EditorToolStrip>()))
                .Returns(items.Length)
                .Verifiable();

            this._dataSourceMock.Setup(m => m.EditorToolStripItemForIndex(It.IsAny<Model.EditorToolStrip>(), It.IsAny<int>()))
                .Returns<Model.EditorToolStrip, int>((inst, index) =>
                {
                    return items[index];
                })
                .Verifiable();

            this._subject = new Model.EditorToolStrip(this._dataSourceMock.Object, null);
        }

        public void Dispose()
        {
            this._subject.Dispose();
        }

        public class Reload : EditorToolStrip
        {
            [Fact]
            public void LoadsResultsFromInstance()
            {
                this._dataSourceMock.Verify();
            }

            [Fact]
            public void LoadsCorrectNumberOfRecords()
            {
                this._subject.Items.Count.Should().Equal(2);
            }

            [Fact]
            public void LoadsItemsInDefinedOrder()
            {
                var expected = new string[] { "Style", "Some Button" };

                for (int i = 0; i < expected.Length; i++)
                {
                    expected[i].Should()
                        .Equal(this._subject.Items[i].Text);
                }
            }
        }

        public class ItemSelected : EditorToolStrip
        {
            [Fact]
            public void DelegatesItemClickEvents()
            {
                var mock = new Mock<IEditorToolStripDelegate>();
                mock.Setup(m => 
                        m.EditorToolStripItemClicked(this._subject, It.IsAny<ToolStripItem>())
                    )
                    .Verifiable();

                this._subject.Delegate = mock.Object;

                var item = this._subject.Items[0] as ToolStripDropDownItem;
                item.DropDownItems[0].PerformClick();

                mock.Verify();
            }
        }
    }
}
