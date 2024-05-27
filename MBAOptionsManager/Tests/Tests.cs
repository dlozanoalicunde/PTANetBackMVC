using Xunit;
using Moq;
using MBAOptionsManager.Controllers;
using Microsoft.EntityFrameworkCore;

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

        [Fact]
        public async Task GetMBAOptions_ReturnsMBAOptions()
        {
            //Arrange
            var context = GetInMemoryDbContext();
            var logger = GetLogger();
            var controller = new MBAOptionsController(logger, context);

            //Act
            var result = controller.GetMbaOptions();

            //Assert
            Assert.NotEmpty(result);
        }
    }
}
