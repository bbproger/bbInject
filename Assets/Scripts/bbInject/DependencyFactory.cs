using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace bb.bbInject
{
    public class DependencyFactory
    {
        public delegate object DependencyDelegate(IDependencyProvider provider);
        
        public static DependencyDelegate FromClass<T>() where T : class, new()
        {
            return provider =>
            {
                Type type = typeof(T);
                T instance = Activator.CreateInstance<T>();
                provider.Inject(instance);
                return instance;
            };
        }

        public static DependencyDelegate FromPrefab<T>(T prefab) where T : MonoBehaviour
        {
            return provider => prefab;
        }

        public static DependencyDelegate FromNewPrefab<T>(T prefab) where T : MonoBehaviour
        {
            return provider =>
            {
                bool wasActive = prefab.gameObject.activeSelf;
                prefab.gameObject.SetActive(false);
                T instance = Object.Instantiate(prefab);
                prefab.gameObject.SetActive(wasActive);
                MonoBehaviour[]
                    children = instance.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (MonoBehaviour child in children)
                {
                    provider.Inject(child);
                }

                instance.gameObject.SetActive(wasActive);
                return instance;
            };
        }

        public static DependencyDelegate FromNewPrefabInResources<T>(string path) where T : MonoBehaviour
        {
            T prefab = Resources.Load<T>(path);
            return FromNewPrefab(prefab);
        }

        public static DependencyDelegate FromInstance<T>(T instance)
        {
            return provider =>
            {
                provider.Inject(instance);
                return instance;
            };
        }

        public static DependencyDelegate FromGameObject<T>(T instance) where T : MonoBehaviour
        {
            return provider =>
            {
                MonoBehaviour[]
                    children = instance.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (MonoBehaviour child in children)
                {
                    provider.Inject(child);
                }

                return instance;
            };
        }
    }
}