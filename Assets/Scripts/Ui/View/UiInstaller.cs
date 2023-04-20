using bb.bbInject;
using bb.Ui.View.Home;
using bb.UiModule;

namespace bb.Ui.View
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