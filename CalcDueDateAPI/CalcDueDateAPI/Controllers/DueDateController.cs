using System;
using System.Collections.Generic;
using System.Web.Http;
using CalcDueDateAPI.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace CalcDueDateAPI.Controllers
{
    public class DueDateController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> CalculateEndDateAsync([FromBody] TaskInfo taskInfo)
        {
            List<DayOfWeek> weekendDays = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };

            List<DateTime> publicHolidays = await GetPublicHolidaysForUKAsync();

            DateTime currentDay = taskInfo.StartDate.Date;
            int remainingWorkingDays = taskInfo.WorkingDays;

            while (remainingWorkingDays > 0)
            {
                if (!weekendDays.Contains(currentDay.DayOfWeek) && !publicHolidays.Contains(currentDay))
                {
                    remainingWorkingDays--;
                }
                currentDay = currentDay.AddDays(1);
            }

            DateTime endDate = currentDay.AddDays(-1);
            return Ok(endDate);
        }

        public async Task<List<DateTime>> GetPublicHolidaysForUKAsync()
        {
            string apiKey = "AIzaSyATQnS_A6aZ7TfooTKk5JbYD7L-rVdgIlw";
            string calendarId = "en.uk%23holiday%40group.v.calendar.google.com";

            string apiUrl = $"https://www.googleapis.com/calendar/v3/calendars/{calendarId}/events?key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<GoogleCalendarApiResponse>(content);

                    List<DateTime> publicHolidays = new List<DateTime>();

                    foreach (var item in apiResponse.Items)
                    {
                        if (item.Start != null && item.Start.Date != null)
                        {
                            publicHolidays.Add(DateTime.Parse(item.Start.Date));
                        }
                    }

                    return publicHolidays;
                }
                else
                {
                    return new List<DateTime>();
                }
            }
        }
    }

    public class GoogleCalendarApiResponse
    {
        public List<GoogleCalendarEvent> Items { get; set; }
    }

    public class GoogleCalendarEvent
    {
        public GoogleCalendarEventStart Start { get; set; }
    }

    public class GoogleCalendarEventStart
    {
        public string Date { get; set; }
    }

}
