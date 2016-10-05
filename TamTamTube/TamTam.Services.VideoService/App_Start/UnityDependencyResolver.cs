using Caterpillar.Core.Application;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TamTam.Services.VideoService
{
    public class UnityDependencyResolver : IDependencyResolver, ICaterpillarDependencyResolver
    {
        private readonly IUnityContainer _container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            this._container = container;
        }

        public object GetService(Type serviceType)
        {
            if (!_container.IsRegistered(serviceType))
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                {
                    return null;
                }
            }
            return _container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }

        public T GetType<T>()
        {
            object service = GetService(typeof(T));
            return (T)service;
        }
    }
}