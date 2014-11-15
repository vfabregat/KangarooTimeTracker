using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using FluentValidation;
using Kangaroo.Infrastructure;
using Kangaroo.Infrastructure.CommandProcessor;
using SimpleInjector;
using SimpleInjector.Extensions;
using Kangaroo.Utils;

namespace Kangaroo
{
    public class DependenciesConfig
    {

        public static void RegisterInstances(Container container)
        {
            var assembly = Assembly.GetExecutingAssembly();


            container.RegisterManyForOpenGeneric(typeof(AbstractValidator<>), assembly);
            container.RegisterManyForOpenGeneric(typeof(ICommandHandler<>), assembly);
            //container.RegisterManyForOpenGeneric(typeof(ISession<>), assembly);
            container.Register<ISession, Session>();
            container.Register<ICommandBus, CommandBus>();
            container.RegisterManyForOpenGeneric(typeof(Lazy<>), assembly);

            //var registrations = assembly.GetExportedTypes()
            //    .Where(t => t.GetInterfaces().Any())
            //    .Select(s => new
            //    {
            //        Service = s.GetInterfaces().Single(),

            //    });

        }
    }
}