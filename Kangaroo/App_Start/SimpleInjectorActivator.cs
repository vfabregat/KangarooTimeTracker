using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Kangaroo.SimpleInjectorActivator), "Start")]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Kangaroo.SimpleInjectorActivator), "PostStart")]
//[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Kangaroo.SimpleInjectorActivator), "Shutdown")]

namespace Kangaroo
{
    public class SimpleInjectorActivator
    {
        private static Container container;
        public static void Start()
        {
            container = new Container();
            DependenciesConfig.RegisterInstances(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();


            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(container));
        }

        public static void PostStart()
        {
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

        }
    }

    public class CustomSimpleInjectorDependencyResolver : IDependencyResolver
    {
        private readonly Container container;
        public CustomSimpleInjectorDependencyResolver(Container container)
        {
            this.container = container;
        }
        public object GetService(Type serviceType)
        {
            return container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances(serviceType);
        }
    }
}