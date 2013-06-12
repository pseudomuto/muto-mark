using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutoMark.Model
{
    public class Unsubscriber<TType> : IDisposable
    {
        private IList<IObserver<TType>> _observers;
        private IObserver<TType> _observer;

        public Unsubscriber(IList<IObserver<TType>> observers, IObserver<TType> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (this._observers != null && this._observers.Contains(this._observer))
            {
                this._observers.Remove(this._observer);
            }
        }
    }
}
