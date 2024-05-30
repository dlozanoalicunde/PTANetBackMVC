using Xunit;
using Moq;
using MBAOptionsManager.Controllers;
using Microsoft.EntityFrameworkCore;
using MBAOptionsManager.Model;
using System.Text.Json;
using Moq.Protected;
using System.Net;
using MBAOptionsManager.Utilities;

namespace MBAOptionsManager.Tests
{
    public class Tests
    {
        private ILogger<MBAOptionsController> GetLogger()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); // More log providers can be added here
            });
            return loggerFactory.CreateLogger<MBAOptionsController>();
        }

        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            return new ApplicationDbContext(options);
        }

        private HttpClient GetMockHttpClient()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var mbaOptions = new List<ExternalMBAOption>
        {
            new ExternalMBAOption
            {
                Country = "USA",
                CountryCode = "US",
                MBAs = new List<ExternalMBA>
                {
                    new ExternalMBA { Code = "MBA001", Name = "Harvard Business School" }
                }
            }
        };
            var responseContent = new StringContent(JsonSerializer.Serialize(mbaOptions), System.Text.Encoding.UTF8, "application/json");

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = responseContent
                });

            return new HttpClient(mockHttpMessageHandler.Object);
        }

        [Fact]
        public async Task GetMBAOptions_ReturnsMBAOptions()
        {
            //Arrange
            var context = GetInMemoryDbContext();
            var logger = GetLogger();
            var httpClient = GetMockHttpClient();
            var controller = new MBAOptionsController(logger, context, new HttpClientFactory(httpClient));

            //Act
            var result = controller.GetMbaOptions();

            //Assert
            Assert.NotEmpty(result.Result.Value);
        }
    }
}
