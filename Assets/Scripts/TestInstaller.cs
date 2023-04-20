using bb.bbInject;
using UnityEngine;

public class TestInstaller : DependencyInstaller
{
    [SerializeField] private Test test;

    public override void Install(Container container)
    {
        container.Bind<Test>().FromInstance(test).AsSingle();
    }
}