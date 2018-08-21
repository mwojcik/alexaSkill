using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response;
using AlexaSkillCommon;
using AlexaSkillCommon.Infrastructure;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Autofac;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(JsonSerializer))]

namespace PerfectGym
{
    public class PerfectGymFunction
    {
        public static Lazy<ILambdaFunctionLifetimeScope> LifetimeScope { get; set; } =
            new Lazy<ILambdaFunctionLifetimeScope>(new LambdaFunctionLifetimeScope<PerfectGymModule>());

        public  SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            using (var innerScope = LifetimeScope.Value.BeginLifetimeScope(context))
            {
                var mediator = innerScope.Resolve<IMediator>();
                context.Logger.LogLine("Request: " + input.Request);
                var result = mediator.HandleRequest(input.Request, context);
                return result;
            }
        }
    }
}