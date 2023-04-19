using System;

namespace bbInject
{
    public class Dependency
    {
        public Type Type { get; set; }
        public bool IsSingleton { get; set; }
        public DependencyFactory.DependencyDelegate Factory { get; set; }
    }
}