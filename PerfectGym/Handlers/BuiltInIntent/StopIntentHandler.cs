using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using AlexaSkillCommon.Infrastructure;
using Amazon.Lambda.Core;

namespace PerfectGym.Handlers.BuiltInIntent
{
    public class StopIntentHandler : IIntentHandler
    {
        public string IntentName => "AMAZON.StopIntent";

        public SkillResponse Handle(IntentRequest intent, ILambdaContext context, Session inputSession)
        {
            return AlexaResponseBuilder.MakeSkillResponse("Stopped", true);
        }
    }
}