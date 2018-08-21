﻿using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using AlexaSkillCommon.Infrastructure;
using Amazon.Lambda.Core;

namespace PerfectGym.Handlers.BuiltInIntent
{
    public class LaunchRequestHandler : IRequestHandler<LaunchRequest>
    {
        public SkillResponse Handle(LaunchRequest inputRequest, ILambdaContext context)
        {
            return AlexaResponseBuilder.MakeSkillResponse("Welcome to PerfectGym. Would you like to book a class ? ", false);
        }
    }
}