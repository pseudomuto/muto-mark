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
    public abstract class TrayMenu : IDisposable
    {
        private Model.TrayMenu _subject = new Model.TrayMenu();
        private Mock<ITrayMenuDataSource> _dataSourceMock;

        public TrayMenu()
        {
            this._dataSourceMock = new Mock<ITrayMenuDataSource>();

            this._dataSourceMock.Setup(d => d.NumberOfToolStripItems(It.IsAny<Model.TrayMenu>()))
                .Returns(2)
                .Verifiable();

            this._dataSourceMock.Setup(d => d.ToolStripItemForIndex(It.IsAny<Model.TrayMenu>(), It.IsInRange<int>(0, 1, Range.Inclusive)))
                .Returns<Model.TrayMenu, int>((inst, index) =>
                {
                    return new ToolStripMenuItem("Item " + index.ToString());
                }).Verifiable();

            this._subject = new Model.TrayMenu(this._dataSourceMock.Object);
        }

        public void Dispose()
        {
            this._subject.Dispose();
        }

        public class Reload : TrayMenu
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
                var expected = new string[] { "Item 0", "Item 1" };

                for (int i = 0; i < expected.Length; i++)
                {
                    expected[i].Should()
                        .Equal(this._subject.Items[i].Text);
                }
            }
        }

        public class ItemSelected : TrayMenu
        {
            [Fact]
            public void DelegatesItemClickEvents()
            {
                var mock = new Mock<ITrayMenuDelegate>();
                mock.Setup(m =>
                        m.TrayMenuItemClicked(this._subject, It.IsAny<ToolStripItem>())
                    )
                    .Verifiable();

                this._subject.Delegate = mock.Object;
                this._subject.Items[1].PerformClick();

                mock.Verify();
            }
        }
    }
}
