using UnityEngine;

namespace bbInject
{
    public class DependencyContext : MonoBehaviour
    {
        [SerializeField] private DependencyInstaller[] installers;
        [SerializeField] private bool initializeOnAwake = true;

        private readonly IDependencyProvider _provider = new DependencyProvider();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (!initializeOnAwake)
            {
                return;
            }

            Setup();
        }


        public void Setup()
        {
            Container container = new Container();
            container.Bind<IDependencyProvider>().FromInstance(_provider).AsSingle();
            foreach (DependencyInstaller installer in installers)
            {
                installer.Install(container);
            }

            _provider.Setup(container);
        }
    }
}