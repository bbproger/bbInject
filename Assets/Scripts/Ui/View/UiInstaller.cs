using bbInject;
using bbInject.UiModule;
using Ui.View.Home;

namespace Ui.View
{
    public class UiInstaller : UiSkeletonInstaller
    {
        private const string ViewPath = "Prefabs/Ui/View";

        protected override void InstallUi(Container container)
        {
            container.BindView<HomeView, HomeViewController>(ViewPath);
        }
    }
}