using System;
using bb.bbInject;
using UnityEngine;

namespace bb.UiModule
{
    public abstract class View : MonoBehaviour
    {
        protected IViewData ViewData { get; private set; }

        public virtual void Setup(IViewData viewData)
        {
            ViewData = viewData;
        }
    }

    public abstract class View<TController> : View where TController : ViewController
    {
        protected TController Controller { get; private set; }
        private ViewController.Factory _controllerFactory;

        [Inject]
        private void Inject(ViewController.Factory factory)
        {
            _controllerFactory = factory;
        }

        public sealed override void Setup(IViewData viewData)
        {
            base.Setup(viewData);
            Controller = _controllerFactory.Create<TController>();
            Setup();
        }

        protected virtual void Setup()
        {
            
        }

        private void OnDestroy()
        {
            (Controller as IDisposable)?.Dispose();
        }
    }
}