using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Alexa.NET.Request.Type;
using Newtonsoft.Json;

namespace PerfectGym
{
    public class GoApiClient
    {
        public string BaseUrl => "perfectgym-go-api-testing.azurewebsites.net/";
        public string Token => "f07ff74d-7172-4331-a9ad-b9e9d4c08734_98f97089-1f8a-420e-b4d8-324b1165e35d";


        public async Task<GoApiResponse<List<UpcomingClassGoApiDto>>> UpcomingClasses(string className,string clubName,
            DateTime dateFrom,DateTime dateTo)
        {
            GoApiResponse<List<UpcomingClassGoApiDto>> result = null;
            using (var restClient = new HttpClient())
            {
                restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                var query = CreateUpcomingClassesQuery(className,clubName,dateFrom,dateTo);
                using (var goApiResult = await restClient.GetAsync(query))
                {
                    var content = await goApiResult.Content.ReadAsStringAsync();

                    if (goApiResult.StatusCode != HttpStatusCode.OK)
                    {
                        result = JsonConvert.DeserializeObject<GoApiResponse<List<UpcomingClassGoApiDto>>>(content, GetJsonSerializerSettings());
                    }

                    result = JsonConvert.DeserializeObject<GoApiResponse<List<UpcomingClassGoApiDto>>>(content, GetJsonSerializerSettings());

                }
            }
            return result;
        }

        private string CreateUpcomingClassesQuery(string className, string clubName,
            DateTime dateFrom, DateTime dateTo)
        {
            var builder = CreateQueryBuilder("/v1.0/Alexa/UpcomingClasses");

            var query = HttpUtility.ParseQueryString(builder.Query);

            query["className"] = className;
            query["clubName"] = clubName;
            query["dateFrom"] = dateFrom.ToString(CultureInfo.InvariantCulture);
            query["dateTo"] = dateTo.ToString(CultureInfo.InvariantCulture);

            builder.Query = query.ToString();
            return builder.ToString();
        }

        private UriBuilder CreateQueryBuilder(string path)
        {
            var builder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Port = -1,
                Host = BaseUrl,
                Path = path
            };
            return builder;
        }

        private static JsonSerializerSettings GetJsonSerializerSettings()
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            return jsonSerializerSettings;
        }

        public async Task<GoApiResponse<List<UpcomingBookingGoApiDto>>> UpcomingBookings()
        {
            GoApiResponse<List<UpcomingBookingGoApiDto>> result = null;
            using (var restClient = new HttpClient())
            {
                restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                var query = CreateQueryBuilder("/v1.0/Alexa/UpcomingBookings").ToString();
                using (var goApiResult = await restClient.GetAsync(query))
                {
                    var content = await goApiResult.Content.ReadAsStringAsync();

                    if (goApiResult.StatusCode != HttpStatusCode.OK)
                    {
                        result = JsonConvert.DeserializeObject<GoApiResponse<List<UpcomingBookingGoApiDto>>>(content, GetJsonSerializerSettings());
                    }

                    result = JsonConvert.DeserializeObject<GoApiResponse<List<UpcomingBookingGoApiDto>>>(content, GetJsonSerializerSettings());

                }
            }
            return result;
        }

        public async Task<GoApiResponse<CancelBookingGoApiResult>> CancelBooking(CancelBookingParams param)
        {
            GoApiResponse<CancelBookingGoApiResult> result = null;
            using (var restClient = new HttpClient())
            {
                restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                var query = CreateQueryBuilder("/v1.0/Alexa/CancelBooking").ToString();
                var json = JsonConvert.SerializeObject(param);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                using (var goApiResult = await restClient.PostAsync(query, stringContent))
                {
                    var content = await goApiResult.Content.ReadAsStringAsync();

                    if (goApiResult.StatusCode != HttpStatusCode.OK)
                    {
                        result = JsonConvert.DeserializeObject<GoApiResponse<CancelBookingGoApiResult>>(content, GetJsonSerializerSettings());
                    }

                    result = JsonConvert.DeserializeObject<GoApiResponse<CancelBookingGoApiResult>>(content, GetJsonSerializerSettings());

                }
            }
            return result;
        }

        public async Task<GoApiResponse<BookClassGoApiResult>> BookClass(BookClassParams param)
        {
            GoApiResponse<BookClassGoApiResult> result = null;
            using (var restClient = new HttpClient())
            {
                restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                var query = CreateQueryBuilder("/v1.0/Alexa/BookClass").ToString();
                var json = JsonConvert.SerializeObject(param);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                using (var goApiResult = await restClient.PostAsync(query, stringContent))
                {
                    var content = await goApiResult.Content.ReadAsStringAsync();

                    if (goApiResult.StatusCode != HttpStatusCode.OK)
                    {
                        result = JsonConvert.DeserializeObject<GoApiResponse<BookClassGoApiResult>>(content, GetJsonSerializerSettings());
                    }

                    result = JsonConvert.DeserializeObject<GoApiResponse<BookClassGoApiResult>>(content, GetJsonSerializerSettings());

                }
            }
            return result;
        }

    }



    public class BookClassParams
    {
        public DateTime ClassStartDate { get; set; }
        public string ClassName { get; set; }
    }

    public class CancelBookingParams
    {
        public string ClassName { get; set; }
        public DateTime ClassStartDate { get; set; }
    }

    public class UpcomingBookingGoApiDto 
    {
        public string ClassName { get; set; }
        public DateTime ClassStartDate { get; set; }
    }

    public class UpcomingClassGoApiDto 
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int AttendeesCount { get; set; }
        public int? AttendeesLimit { get; set; }
        public string ClassName { get; set; }
        public string InstructorName { get; set; }
        public string ClubName { get; set; }
    }

    public class CancelBookingGoApiResult
    {
        public long? ClassBookingId { get; set; }
        public string Status { get; set; }
    }

    public class BookClassGoApiResult
    {
    }



    public class GoApiResponse<TResponse>
    {
        public TResponse Data { get; set; }

        private List<Error> _errors;

        public List<Error> Errors
        {
            get => _errors;
            set => _errors = value ?? new List<Error>();
        }

        public GoApiResponse()
        {
            Errors=new List<Error>();
        }
    }

    public class GoApiResponse : GoApiResponse<object>
    {
        public static GoApiResponse EmptyApiResponse()
        {
            return new GoApiResponse();
        }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

}
