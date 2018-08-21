using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace GarbageCalednar.Infrastructure
{
    public class IntentRequestHandler : IRequestHandler<IntentRequest>
    {
        private readonly IIntenExecutor _intenExecutor;

        public IntentRequestHandler(IIntenExecutor intenExecutor)
        {
            _intenExecutor = intenExecutor;
        }

        public  SkillResponse Handle(IntentRequest inputRequest, ILambdaContext context)
        {
            context.Logger.LogLine($"Execute {inputRequest.Intent.Name} intent");
            return _intenExecutor.Execute(inputRequest.Intent,context);
        }     
    }
}