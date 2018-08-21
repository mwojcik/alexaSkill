using Amazon.Lambda.Core;
using Autofac;

namespace GarbageCalednar
{

    public interface ILambdaFunctionLifetimeScope
    {
        ILifetimeScope BeginLifetimeScope(ILambdaContext context);
    }

    public class LambdaFunctionLifetimeScope : ILambdaFunctionLifetimeScope
    {
        private readonly IContainer _container;


        public LambdaFunctionLifetimeScope()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AlexaModule>();
            _container = containerBuilder.Build();

        }

        public ILifetimeScope BeginLifetimeScope(ILambdaContext context)
        {
            context.Logger.LogLine("Begin lifetime scope");
           return _container.BeginLifetimeScope();
        }
    }
}