using System;
using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace AlexaSkillCommon.Infrastructure
{

    public interface IIntenExecutor
    {
        SkillResponse Execute(Intent inputRequestIntent, ILambdaContext context);
    }

    public class IntentExecutor : IIntenExecutor
    {
        readonly IEnumerable<IIntentHandler> _intentHandlers;

        public IntentExecutor(IEnumerable<IIntentHandler> intentHandlers)
        {
            _intentHandlers = intentHandlers;
        }


        public SkillResponse Execute(Intent inputRequestIntent, ILambdaContext context)
        {
            context.Logger.LogLine($"Intent handlers count: {_intentHandlers.Count()} ");

            var handler = _intentHandlers.FirstOrDefault(x=>x.IntentName == inputRequestIntent.Name);
            if (handler == null)
                throw new ArgumentException("Intent type is not supported.", inputRequestIntent.Name);

            context.Logger.LogLine($"ResolvedHandler: {handler.GetType()} ");
            return handler?.Handle(inputRequestIntent);
        }
    }
}