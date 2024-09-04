using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PTA.Infrastructure.DB;
using Moq;
using PTA.Models;
using PTA.BL.Dtos;
using PTA.BL.Mapper;
using PTA.BL.Contracts;
using System.Security.Cryptography;

namespace PTA.BL.Services.Tests
{
    public class MarketPartiesServiceTests
    {
        private readonly Mock<DataContext> _contextMock;
        private readonly Mock<ILogger<MarketPartiesService>> _loggerMock;
        private readonly MarketPartiesService _marketPartiesService;
        private readonly Mock<IMarketPartiesService> _serviceMock;

        private DistributionSystemOperatorDto defaultDto = new() { DsoName = "Test DsoName", DsoCode = "Test DsoCode", CodingScheme = "Test CodingScheme", Country = "ES" };

        public MarketPartiesServiceTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>().Options;

            _contextMock = new Mock<DataContext>(options);
            _loggerMock = new Mock<ILogger<MarketPartiesService>>(MockBehavior.Loose);
            _serviceMock = new Mock<IMarketPartiesService>(MockBehavior.Strict);
            _marketPartiesService = new MarketPartiesService(_contextMock.Object, _loggerMock.Object);
        }

        //[Fact]
        //public async Task CreateAsync_ActionExecutes_CheckResultType_Returns()
        //{
        //    // Arrange
        //    var _id = 1;
        //    var entity = new DistributionSystemOperator { Id = _id, DsoName = "Test DsoName", DsoCode = "Test DsoCode", CodingScheme = "Test CodingScheme", Country = "ES" };
        //    var dto = new DistributionSystemOperatorDto { DsoName = "Test DsoName", DsoCode = "Test DsoCode", CodingScheme = "Test CodingScheme", Country = "ES" };

        //    _contextMock.Setup(db => db.DistributionSystemOperators.FindAsync(_id)).ReturnsAsync(entity);

        //    // Act
        //    var result = await _marketPartiesService.CreateAsync(dto);

        //    // Assert
        //    result.Should().BeEquivalentTo(dto);
        //}

        [Fact]
        public void DeleteAsync_Returns()
        {
            // Arrange
            var _id = 1;

            _serviceMock.Setup(x => x.DeleteAsync(_id)).Callback(() => { });
            Assert.True(true);
        }

        [Fact]
        public async Task GetDistributionSystemOperatorByIdAsync_Returns_SingleObject()
        {
            // Arrange
            var _id = 1;
            _serviceMock.Setup(x => x.GetDistributionSystemOperatorByIdAsync(_id))
                .ReturnsAsync(new DistributionSystemOperatorDto { DsoName = "Test DsoName", DsoCode = "Test DsoCode", CodingScheme = "Test CodingScheme", Country = "ES" });

            // Act
            var result = await _marketPartiesService.GetDistributionSystemOperatorByIdAsync(_id);

            // Assert
            var okResult = Assert.IsType<DistributionSystemOperatorDto>(result);
            Assert.Equal(defaultDto.CodingScheme, okResult.CodingScheme);
        }

        [Fact]
        public async Task GetDistributionSystemOperatorByIdAsync_Returns_Exception()
        {
            // Arrange
            var _id = 1;
            _serviceMock.Setup(x => x.GetDistributionSystemOperatorByIdAsync(_id))
                .ThrowsAsync(new Exception("My custom exception"));

            // Act
            var result = await _marketPartiesService.GetDistributionSystemOperatorByIdAsync(_id);

            // Assert
            var okResult = Assert.IsType<DistributionSystemOperatorDto>(result);
            Assert.Equal(defaultDto.CodingScheme, okResult.CodingScheme);
        }

        //[Fact]
        //public void GetDistributionSystemOperatorsAsync_ActionExecutes_CheckResultType_Returns()
        //{
        //    // Arrange
        //    var id = 1;
        //    var entity = new DistributionSystemOperator { Id = id, DsoName = "Test DsoName", DsoCode = "Test DsoCode", CodingScheme = "Test CodingScheme", Country = "ES" };
        //    var dto = new DistributionSystemOperatorDto { DsoName = "Test DsoName", DsoCode = "Test DsoCode", CodingScheme = "Test CodingScheme", Country = "ES" };

        //    _contextMock.Setup(db => db.DistributionSystemOperators.Select(e => DistributionSystemOperatorMapper.ToDto(e))
        //            .ToListAsync();).ReturnsAsync(entity);

        //    // Act
        //    var result = await _marketPartiesService.CreateAsync(dto);

        //    // Assert
        //    result.Should().BeEquivalentTo(dto);
        //    Assert.True(true);
        //}
    }
}