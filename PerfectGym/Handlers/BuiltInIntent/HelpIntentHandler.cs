using Alexa.NET.Request;
using Alexa.NET.Response;
using AlexaSkillCommon.Infrastructure;

namespace PerfectGym.Handlers.BuiltInIntent
{
    public class HelpIntentHandler : IIntentHandler
    {
        public string IntentName => "AMAZON.HelpIntent";

        public SkillResponse Handle(Intent intent)
        {
            return AlexaResponseBuilder.MakeSkillResponse("How can I help you ? ", true);
        }
    }
}