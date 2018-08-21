using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace AlexaSkillCommon.Infrastructure
{
    public interface IRequestHandler
    {
    }

    public interface IRequestHandler<in TRequest> : IRequestHandler
        where TRequest : Request
    {
        SkillResponse Handle(TRequest inputRequest, ILambdaContext context);
    }
}