using System;
using System.Reflection;
using AlexaSkillCommon.Infrastructure;
using Autofac;
using Module = Autofac.Module;

namespace AlexaSkillCommon
{
    public class AlexaModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>().As<IMediator>();
            builder.RegisterType<IntentExecutor>().As<IIntenExecutor>();


            HandlerRegistrator.RegisterRequestHandlers(builder, ThisAssembly);
            HandlerRegistrator.RegisterIntentHandlers(builder, ThisAssembly);


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

        }

       
    }

    public static class HandlerRegistrator
    {
        public static void RegisterRequestHandlers(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IRequestHandler>())
                .AsImplementedInterfaces();
        }

        public static void RegisterIntentHandlers(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IIntentHandler>())
                .AsImplementedInterfaces();
        }
    }
}
