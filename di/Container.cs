using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputerGraphics.di
{
    public class Container
    {
        private Dictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public void Register<TInterface, TImpl>() where TImpl : TInterface
        {
            _map[typeof(TInterface)] = typeof(TImpl);
        }

        public T Get<T>()
        {
            return (T) Init(typeof(T));
        }

        private object Init(Type parent)
        {
            if (!_map.TryGetValue(parent, out var type))
            {
                throw new ContainerException("No mapping available for type " + parent);
            }

            if (type.GetConstructors().Length == 0)
            {
                throw new ContainerException("No default ctor available for type " + type);
            }
            var typeCtor = type.GetConstructors()[0];

            var args = typeCtor.GetParameters().Select(v => Init(v.ParameterType)).ToArray();

            return typeCtor.Invoke(args);
        }
    }
}