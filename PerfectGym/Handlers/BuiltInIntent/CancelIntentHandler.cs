using Alexa.NET.Request;
using Alexa.NET.Response;
using AlexaSkillCommon.Infrastructure;

namespace PerfectGym.Handlers.BuiltInIntent
{
    public class CancelIntentHandler : IIntentHandler
    {
        public string IntentName => "AMAZON.CancelIntent";

        public SkillResponse Handle(Intent intent)
        {
            return AlexaResponseBuilder.MakeSkillResponse("Canceled",true);
        }
    }
}