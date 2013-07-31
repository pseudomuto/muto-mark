using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MutoMark.Model.Test.Components
{
    public class WatchDog
    {
        public abstract class WatchDogTest : IDisposable
        {
            protected Model.WatchDog _subject;

            protected virtual string FileName { get { return "./textfile.txt"; } }

            public WatchDogTest()
            {
                File.WriteAllText(this.FileName, "# Header");
                this._subject = new Model.WatchDog(this.FileName);
            }

            public void Dispose()
            {
                if (this._subject != null)
                {
                    this._subject.Dispose();
                }
            }
        }

        public class Constructor : WatchDogTest
        {
            [Fact]
            public void ExpandsFilePath()
            {
                var expected = Path.GetFullPath(this.FileName);
                Assert.Equal(expected, this._subject.FileName);
            }

            [Fact]
            public void GuardsAgainstEmptyFileName()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new Model.WatchDog("");
                });
            }

            [Fact]
            public void VerifiesThatTheFileExists()
            {
                Assert.Throws<FileNotFoundException>(() =>
                {
                    new Model.WatchDog("madethisUp.txt");
                });
            }
        }

        public class Subscribe
        {
            public class WithoutAnySubscribers : WatchDogTest
            {
                protected override string FileName
                {
                    get
                    {
                        return "./no_sub_file.txt";
                    }
                }

                [Fact]
                public void IgnoresFileUpdates()
                {
                    File.WriteAllText(this.FileName, "# New Header");
                }
            }

            public class WhenSubscribersExist : WatchDogTest
            {
                protected override string FileName
                {
                    get
                    {
                        return "./file_with_sub.txt";
                    }
                }

                [Fact]
                public void NotifiesSubscribers()
                {
                    var complete = false;

                    var mock = new Mock<IObserver<Document>>();
                    mock.Setup(m => m.OnNext(It.IsAny<Document>()))
                        .Callback(() => complete = true)
                        .Verifiable();

                    var listener = this._subject.Subscribe(mock.Object);
                    File.WriteAllText(this.FileName, "# New Header");

                    // wait...max 250ms
                    var count = 0;
                    while(!complete && (++count < 25)) Thread.Sleep(10);

                    mock.Verify();
                    listener.Dispose();
                }
            }
        }
    }
}
