using bbInject;

namespace Ui.Module
{
    public class ViewService
    {
        private AssetProviderFactory _assetProviderFactory;

        [Inject]
        private void Inject(AssetProviderFactory assetProviderFactory)
        {
            _assetProviderFactory = assetProviderFactory;
        }

        public TView Show<TView, TController>(IViewData viewData) where TView : View<TController> where TController : ViewController
        {
            TView view = _assetProviderFactory.Create<TView>();
            view.Setup(viewData);
            return view;
        }
    }
}