using CalcDueDateAPI.Controllers;
using CalcDueDateAPI.Models;

namespace DueDate.Tests
{
    [TestFixture]
    public class DueDateControllerTests
    {
        private DueDateController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new DueDateController();
        }

        [Test]
        public async Task CalculateEndDateAsync_ReturnsCorrectEndDate()
        {
            var taskInfo = new TaskInfo
            {
                StartDate = new DateTime(2023, 1, 4),
                WorkingDays = 10
            };

            var result = await _controller.CalculateEndDateAsync(taskInfo);
            var endDate = ((System.Web.Http.Results.OkNegotiatedContentResult<DateTime>)result).Content;

            Assert.That(endDate, Is.EqualTo(new DateTime(2023, 1, 18)));
        }

        [Test]
        public async Task GetPublicHolidaysForUKAsync_ReturnsListOfPublicHolidays()
        {
            var publicHolidays = await _controller.GetPublicHolidaysForUKAsync();

            Assert.NotNull(publicHolidays);
            Assert.IsInstanceOf<List<DateTime>>(publicHolidays);
        }
    }
}
