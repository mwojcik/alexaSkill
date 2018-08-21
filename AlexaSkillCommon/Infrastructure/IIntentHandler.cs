using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace AlexaSkillCommon.Infrastructure
{
    public interface IIntentHandler
    {
        string IntentName { get; }
        SkillResponse Handle(IntentRequest intent, ILambdaContext context, Session inputSession);
    }
}