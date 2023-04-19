using UnityEngine;

namespace bbInject
{
    public abstract class DependencyInstaller : MonoBehaviour
    {
        public abstract void Install(Container container);
    }
}