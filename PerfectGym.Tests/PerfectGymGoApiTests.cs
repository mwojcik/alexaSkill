using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Xunit;

namespace PerfectGym.Tests
{
    public class PerfectGymGoApiTests
    {
        #region UpcomingClasses
        [Fact]
        public async Task UpcomingClasses_Should_Return_Error_For_BadRequest()
        {
            var goApiClient = new GoApiClient();
            var result = await goApiClient.UpcomingClasses("","",new DateTime(2018,8,22), new DateTime(2018, 8, 24));

            Assert.NotNull(result);
            Assert.True(result.Errors.Any());

        }

        [Fact]
        public async Task UpcomingClasses_Should_Return_UpcomingBookings_For_Valid_Request()
        {
            var goApiClient = new GoApiClient();
            var result = await goApiClient.UpcomingClasses("Aerobics", "PerfectFit", new DateTime(2018, 8, 22), new DateTime(2018, 8, 24));

            Assert.NotNull(result);
            Assert.True(result.Data.Any());
        }

        #endregion UpcomingClasses

        #region UpcomingBookings

        [Fact]
        public async Task UpcomingBooking_Should_Return_UpcomingBookings_For_Valid_Request()
        {
            var goApiClient = new GoApiClient();
            var result = await goApiClient.UpcomingBookings();

            Assert.NotNull(result);
            Assert.True(result.Data.Any());

        }

        #endregion UpcomingBookings

        #region CancelBooking
        [Fact]
        public async Task CancelBooking_Should_Return_Error_For_BadRequest()
        {
            var goApiClient = new GoApiClient();
            var result = await goApiClient.CancelBooking(new CancelBookingParams
            {
                ClassName = "aaa",
                ClassStartDate = DateTime.Now
            });

            Assert.NotNull(result);
            Assert.True(result.Errors.Any());

        }

        [Fact]
        public async Task CancelBooking_Should_Return_Success_For_ValidRequest()
        {
            var goApiClient = new GoApiClient();
            var bookClassResult = await goApiClient.BookClass(new BookClassParams
            {
                ClassName = "Aerobics",
                ClassStartDate = new DateTime(2018, 8, 31, 0, 0, 0)
            });
            Assert.NotNull(bookClassResult);
            Assert.False(bookClassResult.Errors.Any());

            var result = await goApiClient.CancelBooking(new CancelBookingParams
            {
                ClassName = "Aerobics",
                ClassStartDate = new DateTime(2018,8,31,0,0,0)
            });
            Assert.NotNull(result);
            Assert.False(result.Errors.Any());
        }

        #endregion CancelBooking


        #region ClassBooking
        [Fact]
        public async Task BookClass_Should_Return_Error_For_BadRequest()
        {
            var goApiClient = new GoApiClient();
            var result = await goApiClient.BookClass(new BookClassParams()
            {
                ClassName = "aaa",
                ClassStartDate = DateTime.Now
            });

            Assert.NotNull(result);
            Assert.True(result.Errors.Any());

        }

        [Fact]
        public async Task BookClass_Should_Return_Success_For_ValidRequest()
        {
            var goApiClient = new GoApiClient();
            var bookClassResult = await goApiClient.BookClass(new BookClassParams
            {
                ClassName = "Aerobics",
                ClassStartDate = new DateTime(2018, 8, 31, 0, 0, 0)
            });
            Assert.NotNull(bookClassResult);
            Assert.False(bookClassResult.Errors.Any());

            var result = await goApiClient.CancelBooking(new CancelBookingParams
            {
                ClassName = "Aerobics",
                ClassStartDate = new DateTime(2018, 8, 31, 0, 0, 0)
            });
            Assert.NotNull(result);
            Assert.False(result.Errors.Any());
        }

        #endregion ClassBooking

    }
}
