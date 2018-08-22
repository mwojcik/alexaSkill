using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using AlexaSkillCommon;
using AlexaSkillCommon.Infrastructure;
using Amazon.Lambda.Core;

namespace PerfectGym.Handlers
{
    public class NextClassesIntent : IIntentHandler
    {
        public string IntentName => "NextClasses";

        public SkillResponse Handle(IntentRequest intentRequest, ILambdaContext context, Session session)
        {
            var intent = intentRequest.Intent;

            if (IsDialogIntentRequest(intentRequest))
            {
                if (IsDialogSequenceComplete(intentRequest) == false)
                {
                    return ResponseBuilder.DialogDelegate(session);
                }
            }

            var response = "ConfirmationStatus: " + intent.ConfirmationStatus + Environment.NewLine;
            var classNameSlot = intent.Slots["className"];
            context.Logger.LogLine(classNameSlot.ToLogFormattedString());



            return ResponseBuilder.Tell("NextClasses executed."+ response);
        }


        private static bool IsDialogIntentRequest(IntentRequest input)
        {
            return !string.IsNullOrEmpty(input.DialogState);
        }

        private static bool IsDialogSequenceComplete(IntentRequest input)
        {
            if (input.DialogState.Equals(AlexaConstants.DialogStarted) || input.DialogState.Equals(AlexaConstants.DialogInProgress))
            {
                return false;
            }

            return input.DialogState.Equals(AlexaConstants.DialogCompleted);
        }
    }
}
