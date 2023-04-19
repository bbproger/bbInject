using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace bbInject
{
    public interface IDependencyProvider
    {
        void Setup(IEnumerable<Dependency> dependencies);
        void Inject(object instance);
        T Resolve<T>() where T : class;
    }

    public static class CastTool
    {
        public static Func<object, object> Caster(Type type)
        {
            var inputObject = Expression.Parameter(typeof(object));
            var inputPara = new[] { inputObject };
            var a = Expression.Lambda<Func<object, object>>(Expression.Convert(inputObject, type), inputPara).Compile();
            return a;
        }
    }

    public class DependencyProvider : IDependencyProvider
    {
        private readonly Dictionary<Type, Dependency> _dependencies = new();
        private readonly Dictionary<Type, object> _singletons = new();


        public void Setup(IEnumerable<Dependency> dependencies)
        {
            foreach (Dependency dependency in dependencies)
            {
                _dependencies.Add(dependency.Type, dependency);
            }

            AutoInject();
        }

        private void AutoInject()
        {
            foreach (KeyValuePair<Type, Dependency> dependency in _dependencies)
            {
                if (!dependency.Value.IsSingleton)
                {
                    continue;
                }

                Resolve(dependency.Key);
            }
        }


        public void Inject(object instance)
        {
            Type type = instance.GetType();
            while (type != null)
            {
                InjectFields(type, instance);
                InjectMethods(type, instance);
                type = type.BaseType;
            }
        }

        private void InjectFields(IReflect reflect, object instance)
        {
            FieldInfo[] fields = reflect.GetFields(
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly | BindingFlags.Instance
            );

            foreach (FieldInfo field in fields)
            {
                if (field.GetCustomAttribute<InjectAttribute>(false) == null)
                {
                    continue;
                }

                field.SetValue(instance, Resolve(field.FieldType));
            }
        }

        private void InjectMethods(IReflect reflect, object instance)
        {
            MethodInfo[] methods = reflect.GetMethods(
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly | BindingFlags.Instance
            );

            foreach (MethodInfo method in methods)
            {
                if (method.GetCustomAttribute<InjectAttribute>(false) == null)
                {
                    continue;
                }

                ParameterInfo[] parameters = method.GetParameters();
                object[] args = new object[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    args[i] = Resolve(parameters[i].ParameterType);
                }

                method.Invoke(instance, args);
            }
        }


        private object Resolve(Type type)
        {
            if (!_dependencies.ContainsKey(type))
            {
                throw new ArgumentException("Type is not a dependency: " + type.FullName);
            }

            Dependency dependency = _dependencies[type];
            if (!dependency.IsSingleton)
            {
                return dependency.Factory(this);
            }

            if (!_singletons.ContainsKey(type))
            {
                object instance = dependency.Factory(this);
                _singletons.Add(type, instance);
            }


            var a = CastTool.Caster(type)(_singletons[type]);
            return a;
            // return _singletons[type];
        }

        public T Resolve<T>() where T : class
        {
            return Resolve(typeof(T)) as T;
        }
    }
}