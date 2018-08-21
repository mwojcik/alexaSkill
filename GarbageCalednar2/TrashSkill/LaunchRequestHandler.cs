using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using GarbageCalednar.Infrastructure;

namespace GarbageCalednar.TrashSkill
{
    public class LaunchRequestHandler : IRequestHandler<LaunchRequest>
    {
        private readonly IIntenExecutor _intenExecutor;

        public LaunchRequestHandler(IIntenExecutor intenExecutor)
        {
            _intenExecutor = intenExecutor;
        }

        public SkillResponse Handle(LaunchRequest inputRequest, ILambdaContext context)
        {
            return AlexaResponseBuilder.MakeSkillResponse("Welcome on TrashCalendar. You can ask when you should be ready with your trash",false);
        }
    }
}