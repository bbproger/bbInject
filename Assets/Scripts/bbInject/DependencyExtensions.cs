using UnityEngine;

namespace bbInject
{
    public static class DependencyExtensions
    {
        public static Container Bind<T>(this Container container)
        {
            Dependency dependency = new Dependency
            {
                Type = typeof(T),
            };
            container.Add(dependency);
            return container;
        }

        public static Container FromClass<T>(this Container container) where T : class, new()
        {
            container[^1].Factory = DependencyFactory.FromClass<T>();
            return container;
        }

        public static void FromPrefab<T>(this Container container,T prefab) where T : MonoBehaviour
        {
            container[^1].Factory = DependencyFactory.FromPrefab(prefab);
            container[^1].IsSingleton = false;
        }

        public static Container FromNewPrefab<T>(this Container container, T prefab) where T : MonoBehaviour
        {
            container[^1].Factory = DependencyFactory.FromNewPrefab(prefab);
            return container;
        }

        public static Container FromGameObject<T>(this Container container, T gameObject) where T : MonoBehaviour
        {
            container[^1].Factory = DependencyFactory.FromGameObject(gameObject);
            return container;
        }

        public static Container FromInstance<T>(this Container container, T instance) where T : class
        {
            container[^1].Factory = DependencyFactory.FromInstance(instance);
            return container;
        }

        public static Container FromMethod<T>(this Container container, DependencyFactory.DependencyDelegate method)
            where T : class
        {
            container[^1].Factory = method;
            return container;
        }

        public static Container FromNewPrefabInResources<T>(this Container container, string path) where T : MonoBehaviour
        {
            container[^1].Factory = DependencyFactory.FromNewPrefabInResources<T>(path);
            return container;
        }


        public static Container AsSingle(this Container container)
        {
            container[^1].IsSingleton = true;
            return container;
        }

        public static Container AsTransient(this Container container)
        {
            container[^1].IsSingleton = false;
            return container;
        }
    }
}