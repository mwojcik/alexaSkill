using Amazon.Lambda.Core;
using Autofac;
using Autofac.Core;

namespace AlexaSkillCommon
{

    public interface ILambdaFunctionLifetimeScope
    {
        ILifetimeScope BeginLifetimeScope(ILambdaContext context);
    }

    public class LambdaFunctionLifetimeScope<TModule> : ILambdaFunctionLifetimeScope where TModule : IModule, new()
    {
        private readonly IContainer _container;


        public LambdaFunctionLifetimeScope()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AlexaModule>();
            containerBuilder.RegisterModule<TModule>();
            _container = containerBuilder.Build();

        }

        public ILifetimeScope BeginLifetimeScope(ILambdaContext context)
        {
            context.Logger.LogLine("Begin lifetime scope");
           return _container.BeginLifetimeScope();
        }
    }
}