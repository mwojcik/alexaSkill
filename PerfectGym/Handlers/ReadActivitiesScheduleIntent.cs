using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Response;
using AlexaSkillCommon.Infrastructure;

namespace PerfectGym.Handlers
{
    public class ReadActivitiesScheduleIntent : IIntentHandler
    {
        public string IntentName => "AMAZON.ReadAction<object@Calendar>";

        public SkillResponse Handle(Intent intent)
        {
            var response = "ConfirmationStatus: " + intent.ConfirmationStatus;

            foreach (var item in intent.Slots)
            {
                response += "key:" + item.Key + " value: " + item.Value; 
            }

            return AlexaResponseBuilder.MakeSkillResponse("ReadAction execued."+ response, false);
        }
    }
}
