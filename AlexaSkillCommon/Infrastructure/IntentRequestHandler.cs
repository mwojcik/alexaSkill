﻿using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace AlexaSkillCommon.Infrastructure
{
    public class IntentRequestHandler : IRequestHandler<IntentRequest>
    {
        private readonly IIntenExecutor _intenExecutor;

        public IntentRequestHandler(IIntenExecutor intenExecutor)
        {
            _intenExecutor = intenExecutor;
        }

        public  SkillResponse Handle(IntentRequest inputRequest, ILambdaContext context, Session session)
        {
            context.Logger.LogLine($"Execute {inputRequest.Intent.Name} intent");
            return _intenExecutor.Execute(inputRequest,context,session);
        }     
    }
}