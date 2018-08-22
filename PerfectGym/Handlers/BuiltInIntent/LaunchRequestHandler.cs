using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using AlexaSkillCommon.Infrastructure;
using Amazon.Lambda.Core;

namespace PerfectGym.Handlers.BuiltInIntent
{
    public class LaunchRequestHandler : IRequestHandler<LaunchRequest>
    {
        public SkillResponse Handle(LaunchRequest inputRequest, ILambdaContext context, Session inputSession)
        {
            return AlexaResponseBuilder.MakeSkillResponse("Welcome to PerfectGym. Would you like to book a class or check your bookings ?", false);
        }
    }
}