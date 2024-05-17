/************************************************************************************************************
 * Julio's tech test for Alicunde Job position
 ************************************************************************************************************/

namespace BankService_UnitTest.Application
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BankService_Application.Managers;
    using BankService_Helper.DTO;

    /// <summary>
    /// Comunitacion manager unit test class
    /// </summary>
    public class ComunicationManagerTest
    {
        /// <summary>
        /// GetBank data method test with valid result
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetBanksDataCorrectResultTest()
        {
            ComunicationManager.Init("https://api.opendata.esett.com");

            List<BankDto> data = await ComunicationManager.GetBanksData("EXP06/Banks");

            Assert.IsNotNull(data);
            Assert.That(data, Is.Not.Empty);
        }
    }
}
