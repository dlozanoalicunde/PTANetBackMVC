
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.UseCases;

namespace Tests.WebAPI
{
    [TestClass]
    public class TestFeeUseCases
    {
        [TestMethod]
        public void TestCalculateFee()
        {
            // Arrange
            var feeUseCases = new FeeUseCases();
            
            // Act
            var result = feeUseCases.CalculateFee(100);
            
            // Assert
            Assert.AreEqual(10, result);  // Example assertion
        }
    }
}
