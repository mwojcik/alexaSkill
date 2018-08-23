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
using AlexaSkillCommon.Infrastructure;
using Amazon.Lambda.Core;

namespace PerfectGym.Handlers
{
    public class UpcomingBookingsIntentHandler : IIntentHandler
    {
        public string IntentName => "UpcomingBookings";

        public SkillResponse Handle(IntentRequest intent, ILambdaContext context, Session inputSession)
        {
            var goApiClient = new GoApiClient();
            var result = goApiClient.UpcomingBookings().Result;

            if (result.Errors.Any())
                return ResponseBuilder.Tell($"{result.Errors.First().Message}");

            if (result.Data.Any() == false)
            {
                var speech = new SsmlOutputSpeech {Ssml = "<speak>I didn't find any bookings.</speak>"};
                var repromptMessage =new PlainTextOutputSpeech {Text = "Would you like to book class now ?"};
                var repromptBody = new Reprompt();
                repromptBody.OutputSpeech = repromptMessage;
                var finalResponse = ResponseBuilder.Ask(speech, repromptBody);
                return finalResponse;
            }

            var day = result.Data.Select(x => x.ClassStartDate).First().ToString("dddd d MMMM",
                CultureInfo.CreateSpecificCulture("en-US"));

            var bookingsResponse = string.Join(',',result.Data.OrderBy(x=>x.ClassStartDate).
                Select(x => $" {x.ClassName} at {x.ClassStartDate.ToShortTimeString()}"));



            var speech1 = new SsmlOutputSpeech { Ssml = $"<speak>{GetFirstPartResponse(result.Data.Count(), day)}{bookingsResponse}</speak>" };
            var repromptMessage1 = new PlainTextOutputSpeech { Text = "" };
            var repromptBody1 = new Reprompt();
            repromptBody1.OutputSpeech = repromptMessage1;
            var finalResponse1 = ResponseBuilder.Ask(speech1, repromptBody1);
            return finalResponse1;

          //  return ResponseBuilder.Tell($"{GetFirstPartResponse(result.Data.Count(), day)}{bookingsResponse}");

        }

        public static string GetFirstPartResponse(int number, string date)
        {
            switch (number)
            {
                case 0:
                    return "I didn't find any bookings.";
                case 1:
                    return ForMoreThanOne("one", date);
                case 2:
                    return ForMoreThanOne("two", date);
                case 3:
                    return ForMoreThanOne("three", date);
                case 4:
                    return ForMoreThanOne("four", date);
                case 5:
                    return ForMoreThanOne("five", date);
                case 6:
                    return ForMoreThanOne("six", date);
                case 7:
                    return ForMoreThanOne("seven", date);
                case 8:
                    return ForMoreThanOne("eight", date);
                case 9:
                    return ForMoreThanOne("nine", date);
                case 10:
                    return ForMoreThanOne("ten", date);

                default: return ForMoreThanOne("more then ten", date);
            }
        }

        private static string ForMoreThanOne(string howMany,string date)
        {
            return $"You have {howMany} bookings on {date}. ";
        }
    }
}
