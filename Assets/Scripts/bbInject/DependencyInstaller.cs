using UnityEngine;

namespace bb.bbInject
{
    public abstract class DependencyInstaller : MonoBehaviour
    {
        public abstract void Install(Container container);
    }
}