using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Core;
using GarbageCalednar.Infrastructure;
using Module = Autofac.Module;

namespace GarbageCalednar
{
    public class AlexaModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>().As<IMediator>();
            builder.RegisterType<IntentExecutor>().As<IIntenExecutor>();

            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes()
                .Where(x => x.IsAssignableTo<IRequestHandler>())
                .AsImplementedInterfaces();


            builder.Register<Func<Type, IRequestHandler>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return t =>
                {
                    var handlerType = typeof(IRequestHandler<>).MakeGenericType(t);
                    return (IRequestHandler)ctx.Resolve(handlerType);
                };
            });

            builder.RegisterType<Mediator>().As<IMediator>();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsAssignableTo<IIntentHandler>())
                .AsImplementedInterfaces();
        }
    }
}
