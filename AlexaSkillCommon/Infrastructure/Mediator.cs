using System;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace AlexaSkillCommon.Infrastructure
{
    public interface IMediator
    {
        SkillResponse HandleRequest<TRequest>(TRequest requestType, ILambdaContext context, Session inputSession) where TRequest : Request;
    }

    public class Mediator : IMediator
    {
        private readonly Func<Type, IRequestHandler> _handlersFactory;

        public Mediator(Func<Type, IRequestHandler> handlersFactory)

        {
            _handlersFactory = handlersFactory;
        }

        public SkillResponse HandleRequest<TRequest>(TRequest request, ILambdaContext context, Session inputSession)
            where TRequest : Request
        {
            context.Logger.LogLine($"Request: {request.GetType()}");

            var handler = (dynamic)_handlersFactory(request.GetType());
            context.Logger.LogLine($"handler: {handler?.GetType()}");

            return handler.Handle((dynamic)request,context, inputSession);
        }
    }

}
