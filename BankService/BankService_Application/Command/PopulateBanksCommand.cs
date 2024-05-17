/************************************************************************************************************
 * Julio's tech test for Alicunde Job position
 ************************************************************************************************************/

namespace BankService_Application.Command
{
    using MediatR;

    /// <summary>
    /// command request class for populate bank business
    /// </summary>
    public class PopulateBanksCommand : IRequest<bool>
    {
        public string BanksApiSource { get; }

        public string EndpointPath { get; }

        public PopulateBanksCommand(string apiSource, string path)
        { 
            BanksApiSource = apiSource;
            EndpointPath = path;
        }
}
}
