using System;
using System.Collections.Generic;
using System.Text;
using AlexaSkillCommon;
using AlexaSkillCommon.Infrastructure;
using Autofac;

namespace PerfectGym
{
    public class PerfectGymModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            HandlerRegistrator.RegisterRequestHandlers(builder, ThisAssembly);
            HandlerRegistrator.RegisterIntentHandlers(builder, ThisAssembly);
        }
    }
}
