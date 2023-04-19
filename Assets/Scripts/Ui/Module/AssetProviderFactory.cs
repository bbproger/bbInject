using bbInject;

namespace Ui.Module
{
    public class AssetProviderFactory
    {
        private IDependencyProvider _provider;
        private UiContainer _container;

        [Inject]
        private void Inject(IDependencyProvider provider, UiContainer container)
        {
            _provider = provider;
            _container = container;
        }

        public TView Create<TView>() where TView : View
        {
            TView view = _provider.Resolve<TView>();
            view.transform.SetParent(_container.ViewContainer, false);
            return view;
        }
    }
}