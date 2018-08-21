using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using AlexaSkillCommon.Infrastructure;
using Amazon.Lambda.Core;

namespace PerfectGym.Handlers.BuiltInIntent
{
    public class CancelIntentHandler : IIntentHandler
    {
        public string IntentName => "AMAZON.CancelIntent";

        public SkillResponse Handle(IntentRequest intent, ILambdaContext context, Session inputSession)
        {
            return AlexaResponseBuilder.MakeSkillResponse("Canceled",true);
        }
    }
}