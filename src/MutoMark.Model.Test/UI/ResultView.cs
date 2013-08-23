using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Should.Fluent;

namespace MutoMark.Model.Test.UI
{
    public abstract class ResultView : IDisposable
    {
        private Model.ResultView _subject;
        private Mock<IResultViewDataSource> _dataSourceMock;

        public ResultView()
        {
            this._dataSourceMock = new Mock<IResultViewDataSource>();
            this._dataSourceMock.Setup(m => m.HTMLForResultView(It.IsAny<Model.ResultView>()))
                .Returns("<p>Original HTML</p>")
                .Verifiable();

            this._subject = new MockResultView(this._dataSourceMock.Object);
        }

        public void Dispose()
        {
            this._subject.Dispose();
        }

        public class Reload : ResultView
        {
            [Fact]
            public void InitiallyShowsModelFromDataSource()
            {
                this._subject.HTML.Should()
                    .Equal("<p>Original HTML</p>");

                this._dataSourceMock.Verify();
            }

            [Fact]
            public void WhenDataSourceIsChangedReloadWorks()
            {
                var mock = new Mock<IResultViewDataSource>();
                mock.Setup(m => m.HTMLForResultView(this._subject))
                    .Returns("<p>New Text</p>")
                    .Verifiable();

                this._subject.DataSource = mock.Object;
                this._subject.Reload();

                this._subject.HTML.Should()
                    .Equal("<p>New Text</p>");

                mock.Verify();
            }
        }
    }

    class MockResultView : Model.ResultView
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
