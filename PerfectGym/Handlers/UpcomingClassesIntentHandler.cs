using System;
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
    public class UpcomingClassesIntentHandler : IIntentHandler
    {
        public string IntentName => "UpcomingClasses";

        public SkillResponse Handle(IntentRequest intentRequest, ILambdaContext context, Session session)
        {
            if (IsDialogIntentRequest(intentRequest))
            {
                if (IsDialogSequenceComplete(intentRequest) == false)
                {
                    return ResponseBuilder.DialogDelegate(session);
                }
            }

            var intent = intentRequest.Intent;
            var dateSlotValue = intent.Slots["date"].Value;
            var clubNameSlotValue = intent.Slots["clubName"].Value;
            var classNameSlotValue = intent.Slots["className"].Value;

            var date = NodaTime.AmazonDate.AmazonDateParser.Parse(dateSlotValue);

            var dateFrom = date.From.ToDateTimeUnspecified();
            var dateTo = date.To.ToDateTimeUnspecified();
            if (dateFrom == dateTo)
            {
                dateFrom = new DateTime(dateFrom.Year,dateFrom.Month,dateFrom.Day,0,0,0);
                dateTo = new DateTime(dateTo.Year, dateTo.Month, dateTo.Day, 23, 59, 59);
            }

            var goApiClient = new GoApiClient();
            var result = goApiClient.UpcomingClasses(classNameSlotValue,clubNameSlotValue, dateFrom,dateTo).Result;

            if (result.Errors.Any())
                return ResponseBuilder.Tell($"{result.Errors.First().Message}");

           

            var bookingsResponse = string.Join(',',result.Data.OrderBy(x=>x.StartDate).
                Select(x =>
                {
                    var startDate = x.StartDate.ToString("dddd dd MMMM hh:mm tt",
                        CultureInfo.CreateSpecificCulture("en-US"));
                    return $" {x.ClassName} on {startDate}";
                }));

            var speech = new SsmlOutputSpeech { Ssml = $"<speak>{ GetFirstPartResponse(result.Data.Count())}{ bookingsResponse}</speak>" };
            var repromptMessage = new PlainTextOutputSpeech { Text = "Would you like to book one ?" };
            var repromptBody = new Reprompt();
            repromptBody.OutputSpeech = repromptMessage;
            var finalResponse = ResponseBuilder.Ask(speech, repromptBody);
            return finalResponse;

            //return ResponseBuilder.Ask($"{GetFirstPartResponse(result.Data.Count())}{bookingsResponse}",
            //    new Reprompt("Whould you like to book one? "));

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

        public static string GetFirstPartResponse(int number)
        {
            switch (number)
            {
                case 0:
                    return "I didn't find any classes.";
               

                default: return "I found the following classes.";
            }
        }

    
    }
}
