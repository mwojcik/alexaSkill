﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;
using AlexaSkillCommon;
using AlexaSkillCommon.Infrastructure;
using Amazon.Lambda.Core;

namespace PerfectGym.Handlers
{
    public class BookClassIntentHandler : IIntentHandler
    {
        public string IntentName => "BookClass";

        public SkillResponse Handle(IntentRequest intentRequest, ILambdaContext context, Session session)
        {
            var goApiClient = new GoApiClient();
            var intent = intentRequest.Intent;

            if (IsDialogIntentRequest(intentRequest))
            {
                if (IsDialogSequenceComplete(intentRequest) == false)
                {
                    return ResponseBuilder.DialogDelegate(session);
                }
            }

            var dateSlotValue = intent.Slots["date"].Value;
            var timeSlotValue = intent.Slots["time"].Value;
            var classNameSlotValue = intent.Slots["className"].Value;
            var time = Convert.ToDateTime(timeSlotValue);


            context.Logger.LogLine($"Date:{dateSlotValue}");

            var localBindDate = NodaTime.AmazonDate.AmazonDateParser.Parse(dateSlotValue);
            var dateFrom = localBindDate.From.ToDateTimeUnspecified();
            var bookingDateTime = new DateTime(dateFrom.Year,dateFrom.Month,dateFrom.Day,time.Hour,time.Minute,time.Second);

            var result = goApiClient.BookClass(new BookClassParams
             {
                 ClassStartDate = bookingDateTime,
                 ClassName = classNameSlotValue
            }).Result;

            return ResponseBuilder.Tell(result.Errors.Any() ? "There was an error booking a class. Please try again." : "Classes was booked.");
        }

        private static bool IsDialogIntentRequest(IntentRequest input)
        {
            return !string.IsNullOrEmpty(input.DialogState);
        }

        private static bool IsDialogSequenceComplete(IntentRequest input)
        {
            if (input.DialogState.Equals(AlexaConstants.DialogStarted) ||
                input.DialogState.Equals(AlexaConstants.DialogInProgress))
            {
                return false;
            }

            return input.DialogState.Equals(AlexaConstants.DialogCompleted);
        }


    }
}
