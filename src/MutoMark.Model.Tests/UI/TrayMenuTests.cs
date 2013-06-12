using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MutoMark.Model.UI;
using Moq;
using System.Windows.Forms;
using System.Linq;

namespace MutoMark.Model.Tests.UI
{
    [TestClass]
    public class TrayMenuTests
    {
        private TrayMenu _subject = new TrayMenu();
        private Mock<ITrayMenuDataSource> _dataSourceMock;

        [TestInitialize]
        public void TestInit()
        {
            this._dataSourceMock = new Mock<ITrayMenuDataSource>();
            
            this._dataSourceMock.Setup(d => d.NumberOfToolStripItems(It.IsAny<TrayMenu>()))
                .Returns(2)
                .Verifiable();

            this._dataSourceMock.Setup(d => d.ToolStripItemForIndex(It.IsAny<TrayMenu>(), It.IsInRange<int>(0, 1, Range.Inclusive)))
                .Returns<TrayMenu, int>((inst, index) =>
                {
                    return new ToolStripMenuItem("Item " + index.ToString());
                }).Verifiable();

            this._subject = new TrayMenu(this._dataSourceMock.Object);
        }

        [TestMethod]
        public void TrayMenu_ReliesOnDataSource()
        {
            var expected = new string[] {
                "Item 0",
                "Item 1"
            };

            Assert.AreEqual(expected.Length, this._subject.Items.Count);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], this._subject.Items[i].Text);
            }

            this._dataSourceMock.Verify();
        }

        [TestMethod]
        public void TrayMenu_DelegatesClickEvents()
        {
            var mock = new Mock<ITrayMenuDelegate>();
            mock.Setup(m => m.TrayMenuItemClicked(this._subject, It.IsAny<ToolStripItem>())).Verifiable();

            this._subject.Delegate = mock.Object;
            this._subject.Items[1].PerformClick();

            mock.Verify();
        }
    }
}
