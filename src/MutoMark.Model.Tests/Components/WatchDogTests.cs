using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Moq;
using System.Threading;

namespace MutoMark.Model.Tests.Components
{
    [TestClass]
    public class WatchDogTests
    {
        private WatchDog _subject;

        [TestInitialize]
        public void TestInit()
        {
            File.WriteAllText(".\\textfile.txt", "# Header");
            this._subject = new WatchDog(".\\textfile.txt", null);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this._subject.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WatchDog_RequiresFileName()
        {
            new WatchDog("", null);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void WatchDog_RequiresExistingFile()
        {
            new WatchDog(".\\some_file.txt", null);
        }

        [TestMethod]
        public void WatchDog_ExpandsFilePath()
        {
            var expected = Path.GetFullPath(".\\textfile.txt");
            Assert.AreEqual(expected, this._subject.FileName);
        }

        [TestMethod]
        public void WatchDog_NotifiesObserversOnChange()
        {
            var newContents = "# New Header";
            var complete = false;

            var mock = new Mock<IObserver<Document>>();
            mock.Setup(m => m.OnNext(It.IsAny<Document>())).Callback(() =>
            {
                complete = true;
            }).Verifiable();

            var listener = this._subject.Subscribe(mock.Object);
            File.WriteAllText(".\\textfile.txt", newContents);
            
            while (!complete)
            {
                Thread.Sleep(10);
            }

            mock.Verify();
            listener.Dispose();
        }
    }
}
