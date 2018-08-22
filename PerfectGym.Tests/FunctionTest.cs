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
