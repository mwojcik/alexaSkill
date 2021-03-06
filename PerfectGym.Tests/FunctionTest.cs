using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.TestUtilities;
using Xunit;

namespace PerfectGym.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void LaunchRequest_Should_Return_WelcomeMessage()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var context = CreateContext();
            var request = new SkillRequest();
            request.Session = new Session();
            request.Request = new LaunchRequest();

            var upperCase = ExecuteFunction(request, context);

            var response = upperCase.Response.OutputSpeech as PlainTextOutputSpeech;
            Assert.NotNull(response);
            Assert.Equal("Welcome to PerfectGym. Would you like to book a class or check your bookings ?", response.Text);
        }


        [Fact]
        public void UpcomingBookings_Should_Return_UpcomingClassesList()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var context = CreateContext();
            var request = new SkillRequest();
            request.Session = new Session();
            request.Request = new IntentRequest
            {
                Intent = new Intent
                {
                    Name = "UpcomingClasses",Slots = new Dictionary<string, Slot>()
                    {
                        {"clubName",new Slot{Name = "clubName",Value = "PerfectFit"}},
                        {"className",new Slot{Name = "className",Value = "Zumba"}},
                        {"date",new Slot{Name = "date",Value = "2015-11-25"}}
                    }
                }
            };

            var upperCase = ExecuteFunction(request, context);

            var response = upperCase.Response.OutputSpeech as SsmlOutputSpeech;
            Assert.NotNull(response);
        }

        [Fact]
        public void UpcomingBookings_Should_Return_UpcomingBookingsList()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var context = CreateContext();
            var request = new SkillRequest();
            request.Session = new Session();
            request.Request = new IntentRequest
            {
                Intent = new Intent
                {
                    Name = "UpcomingBookings"
                }
            };

            var upperCase = ExecuteFunction(request, context);

            var response = upperCase.Response.OutputSpeech as SsmlOutputSpeech;
            Assert.NotNull(response);
        }



        private static SkillResponse ExecuteFunction(SkillRequest request, TestLambdaContext context)
        {
            var function = new PerfectGymFunction();
            return function.FunctionHandler(request, context);
        }

        private static TestLambdaContext CreateContext()
        {
            var context = new TestLambdaContext();
            context.Logger = new TestLambdaLogger();
            return context;
        }
    }
}
