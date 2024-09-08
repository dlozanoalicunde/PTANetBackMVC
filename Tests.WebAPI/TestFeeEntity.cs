
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;

namespace Tests.WebAPI
{
    [TestClass]
    public class TestFeeEntity
    {
        [TestMethod]
        public void TestFeeInitialization()
        {
            // Arrange
            var fee = new Fee { Amount = 100 };

            // Act
            var amount = fee.Amount;

            // Assert
            Assert.AreEqual(100, amount);
        }
    }
}
