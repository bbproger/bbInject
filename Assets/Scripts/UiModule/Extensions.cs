using bb.bbInject;

namespace bb.UiModule
{
    public static class Extensions
    {
        public static void BindView<TView, TController>(this Container container, string path)
            where TView : View<TController>
            where TController : ViewController, new()
        {
            string prefabPath = $"{path}/{typeof(TView).Name}";
            container.Bind<TView>().FromNewPrefabInResources<TView>(prefabPath).AsTransient();
            container.Bind<TController>().FromClass<TController>().AsTransient();
        }
    }
}