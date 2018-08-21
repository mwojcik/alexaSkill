using System.Collections;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using GarbageCalednar.Infrastructure;

namespace GarbageCalednar.TrashSkill
{
    public class BinsIntentHandler : IIntentHandler
    {
        public string IntentName => "Bins";

        public SkillResponse Handle(Intent intent)
        {
        
            var binDate = intent.Slots["binDate"].Value;
            var localBindDate = NodaTime.AmazonDate.AmazonDateParser.Parse(binDate);
            var dateFrom=  localBindDate.From.ToDateTimeUnspecified();
            var dateTo = localBindDate.To.ToDateTimeUnspecified();

            var nextBeans = TrashCalendar.TrashMonthInfos
                .Where(x => x.Date >= dateFrom && x.Date <= dateTo).OrderBy(x=>x.Date).ToList();

            var response = "";
            foreach (var trashMonthInfo in nextBeans)
            {
                response +=
                    $"Prepare {trashMonthInfo.TrashType} for {trashMonthInfo.Date.DayOfWeek} on {trashMonthInfo.Date.ToShortDateString()}.";
            }

            return nextBeans.Any() ? AlexaResponseBuilder.MakeSkillResponse(response, false, response) :
                AlexaResponseBuilder.MakeSkillResponse("During this period, they don't take any trash. ", true);
        }

      
    }
}