using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Autofac;
using GarbageCalednar.Infrastructure;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(JsonSerializer))]

namespace GarbageCalednar
{
    public class GarbageCalendarFunction
    {
        public static Lazy<ILambdaFunctionLifetimeScope> LifetimeScope { get; set; } = 
            new Lazy<ILambdaFunctionLifetimeScope>(new LambdaFunctionLifetimeScope());

        public async Task<SkillResponse> FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var restClient = new HttpClient();
            restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "0bbff577-933b-426c-8f68-05e3d8aba80c_87c48197-484c-4536-93ff-519d985b61c6");
            var goApiResult = await restClient.GetAsync("http://goapi2.perfectgym.com/v1.0/Classes/Classes?timestamp=0");

            if (goApiResult.IsSuccessStatusCode)
            {
                context.Logger.LogLine("Request: " + await goApiResult.Content.ReadAsStringAsync());

            }

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