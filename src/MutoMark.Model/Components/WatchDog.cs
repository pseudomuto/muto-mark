using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutoMark.Model
{
    public class WatchDog : IObservable<Document>, IDisposable
    {
        public string FileName { get; private set; }
        public IMarkdownProcessor Processor { get; private set; }

        private FileSystemWatcher _watcher;
        private readonly object _syncLock = new object();
        private IList<IObserver<Document>> _observers = new List<IObserver<Document>>();

        public WatchDog(string fileName, IMarkdownProcessor processor)
        {
            // null or default...
            this.Processor = processor ?? new DefaultProcessor();

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(fileName);
            }

            this.FileName = Path.GetFullPath(fileName);
            if (!File.Exists(this.FileName))
            {
                throw new FileNotFoundException();
            }

            this.StartMonitoring();
        }

        private void StartMonitoring()
        {
            this._watcher = new FileSystemWatcher(Path.GetDirectoryName(this.FileName), Path.GetFileName(this.FileName));
            
            this._watcher.Changed += (o, e) =>
            {
                if (e.ChangeType == WatcherChangeTypes.Changed)
                {
                    lock (this._syncLock)
                    {
                        // notify
                        var source = this.GetSource();
                        var doc = new Document(source, this.Processor);

                        foreach (var listener in this._observers)
                        {                            
                            listener.OnNext(doc);
                        }
                    }
                }
            };

            this._watcher.EnableRaisingEvents = true;
        }

        private string GetSource()
        {
            using (var fs = new FileStream(this.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(fs))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public IDisposable Subscribe(IObserver<Document> observer)
        {
            this._observers.Add(observer);
            return new Unsubscriber<Document>(this._observers, observer);
        }

        public void Dispose()
        {
            this._watcher.Dispose();
        }
    }
}
