using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows.Forms;

namespace MutoMark.Model.Tests.UI
{
    [TestClass]
    public class EditorToolStripTests
    {
        private EditorToolStrip _subject;
        private Mock<IEditorToolStripDataSource> _dataSourceMock;

        #region [Init]
        
        [TestInitialize]
        public void TestInit()
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
            this._dataSourceMock.Setup(m => m.NumberOfEditorToolStripItems(It.IsAny<EditorToolStrip>()))
                .Returns(items.Length)
                .Verifiable();

            this._dataSourceMock.Setup(m => m.EditorToolStripItemForIndex(It.IsAny<EditorToolStrip>(), It.IsAny<int>()))
                .Returns<EditorToolStrip, int>((inst, index) =>
                {
                    return items[index];
                }).Verifiable();

            this._subject = new EditorToolStrip(this._dataSourceMock.Object, null);
        }

        #endregion

        [TestMethod]
        public void EditorToolStrip_ReliesOnDataSource()
        {
            var expected = new string[] {
                "Style",
                "Some Button"
            };

            Assert.AreEqual(expected.Length, this._subject.Items.Count);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], this._subject.Items[i].Text);
            }

            this._dataSourceMock.Verify();
        }

        [TestMethod]
        public void EditorToolStrip_DelegatesClicks()
        {
            var mock = new Mock<IEditorToolStripDelegate>();
            mock.Setup(m => m.EditorToolStripItemClicked(this._subject, It.IsAny<ToolStripItem>()))
                .Verifiable();

            this._subject.Delegate = mock.Object;

            (this._subject.Items[0] as ToolStripDropDownButton).DropDownItems[0].PerformClick();

            mock.Verify();
        }
    }
}
