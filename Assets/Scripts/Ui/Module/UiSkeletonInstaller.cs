using System;
using bbInject;
using UnityEngine;

namespace Ui.Module
{
    [Serializable]
    public class UiContainer
    {
        [SerializeField] private RectTransform viewContainer;
        [SerializeField] private RectTransform popupContainer;

        public RectTransform ViewContainer => viewContainer;

        public RectTransform PopupContainer => popupContainer;
    }

    public abstract class UiSkeletonInstaller : DependencyInstaller
    {
        [SerializeField] private UiContainer uiContainer;

        public sealed override void Install(Container container)
        {
            container.Bind<UiContainer>().FromInstance(uiContainer).AsSingle();
            container.Bind<ViewController.Factory>().FromClass<ViewController.Factory>().AsSingle();
            container.Bind<AssetProviderFactory>().FromClass<AssetProviderFactory>().AsSingle();

            InstallUi(container);

            container.Bind<ViewService>().FromClass<ViewService>().AsSingle();
        }

        protected abstract void InstallUi(Container container);
    }
}