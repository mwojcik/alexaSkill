using System;
using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace AlexaSkillCommon.Infrastructure
{

    public interface IIntenExecutor
    {
        SkillResponse Execute(IntentRequest inputRequestIntent, ILambdaContext context, Session session);
    }

    public class IntentExecutor : IIntenExecutor
    {
        readonly IEnumerable<IIntentHandler> _intentHandlers;

        public IntentExecutor(IEnumerable<IIntentHandler> intentHandlers)
        {
            _intentHandlers = intentHandlers;
        }


        public SkillResponse Execute(IntentRequest inputRequestIntent, ILambdaContext context, Session session)
        {
            context.Logger.LogLine($"Intent handlers count: {_intentHandlers.Count()} ");

            var handler = _intentHandlers.FirstOrDefault(x=>x.IntentName == inputRequestIntent.Intent.Name);
            if (handler == null)
                throw new ArgumentException("Intent type is not supported.", inputRequestIntent.Intent.Name);

            context.Logger.LogLine($"ResolvedHandler: {handler.GetType()} ");
            return handler?.Handle(inputRequestIntent, context,session);
        }
    }
}