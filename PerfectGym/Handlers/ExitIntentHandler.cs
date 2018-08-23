using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using AlexaSkillCommon.Infrastructure;
using Amazon.Lambda.Core;

namespace PerfectGym.Handlers.BuiltInIntent
{
    public class ExitIntentHandler : IIntentHandler
    {
        public string IntentName => "Exit";

        public SkillResponse Handle(IntentRequest intent, ILambdaContext context, Session inputSession)
        {
            return AlexaResponseBuilder.MakeSkillResponse("Bye", true);
        }
    }
}