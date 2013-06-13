using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MutoMark.Model.Tests.UI
{
    [TestClass]
    public class ResultViewTests
    {
        private ResultView _subject;
        private Mock<IResultViewDataSource> _dataSourceMock;

        [TestInitialize]
        public void TestInit()
        {
            this._dataSourceMock = new Mock<IResultViewDataSource>();
            this._dataSourceMock.Setup(m => m.HTMLForResultView(It.IsAny<ResultView>()))
                .Returns("<p>Original HTML</p>")
                .Verifiable();

            this._subject = new MockResultView(this._dataSourceMock.Object);
        }

        [TestMethod]
        public void ResultView_ReliesOnDataSource()
        {
            Assert.AreEqual("<p>Original HTML</p>", this._subject.HTML);
            this._dataSourceMock.Verify();
        }

        [TestMethod]
        public void ResultView_ReloadsFromDataSource()
        {
            var mock = new Mock<IResultViewDataSource>();
            mock.Setup(m => m.HTMLForResultView(this._subject))
                .Returns("<p>New Text</p>")
                .Verifiable();

            this._subject.DataSource = mock.Object;
            this._subject.Reload();

            Assert.AreEqual("<p>New Text</p>", this._subject.HTML);
            mock.Verify();
        }
    }

    class MockResultView : ResultView
    {
        public MockResultView(IResultViewDataSource ds)
            : base(ds)
        {
        }

        protected override void SetDocumentText()
        {
            // don't use the browser control...
        }
    }
}
