using Alexa.NET.Request;
using Alexa.NET.Response;

namespace AlexaSkillCommon.Infrastructure
{
    public interface IIntentHandler
    {
        string IntentName { get; }
        SkillResponse Handle(Intent intent);
    }
}