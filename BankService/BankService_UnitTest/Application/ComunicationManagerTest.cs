namespace BankService_UnitTest.Application
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BankService_Application.Managers;
    using BankService_Helper.DTO;

    public class ComunicationManagerTest
    {
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
