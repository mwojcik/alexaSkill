using Alexa.NET.Request;
using Alexa.NET.Response;
using AlexaSkillCommon.Infrastructure;

namespace PerfectGym.Handlers.BuiltInIntent
{
    public class StopIntentHandler : IIntentHandler
    {
        public string IntentName => "AMAZON.StopIntent";

        public SkillResponse Handle(Intent intent)
        {
            return AlexaResponseBuilder.MakeSkillResponse("Stopped", true);
        }
    }
}