using System;
using bb.bbInject;

namespace bb.UiModule
{
    public abstract class ViewController : IDisposable
    {
        public class Factory
        {
            private IDependencyProvider _provider;

            [Inject]
            private void Inject(IDependencyProvider provider)
            {
                _provider = provider;
            }

            public TController Create<TController>() where TController : ViewController
            {
                return _provider.Resolve<TController>();
            }
        }


        void IDisposable.Dispose()
        {
            Dispose();
        }

        protected virtual void Dispose()
        {
        }
    }
}