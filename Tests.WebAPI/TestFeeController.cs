
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Tests.WebAPI
{
    [TestClass]
    public class TestFeeController
    {
        [TestMethod]
        public void TestGetFee()
        {
            // Arrange
            var controller = new FeeController();
            
            // Act
            var result = controller.GetFee(1) as OkObjectResult;
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}
