/************************************************************************************************************
 * Julio's tech test for Alicunde Job position
 ************************************************************************************************************/

namespace BankService_Application.Command
{
    using System.Threading;
    using System.Threading.Tasks;
    using BankService_Application.Managers;
    using BankService_Domain;
    using BankService_Helper.DTO;
    using MediatR;

    /// <summary>
    /// Request command hablder class for pupulate banks logic
    /// </summary>
    public class PopulateBanksCommandHandler : IRequestHandler<PopulateBanksCommand, bool>
    {
        private readonly IBankRepository _bankRepository;

        /// <summary>
        /// Primary constructor
        /// </summary>
        /// <param name="bankRepository"></param>
        public PopulateBanksCommandHandler(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        /// <summary>
        /// Handler method to resolve populate bank logic
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(PopulateBanksCommand request, CancellationToken cancellationToken)
        {
            ComunicationManager.Init(request.BanksApiSource);

            List<BankDto> data = await ComunicationManager.GetBanksData(request.EndpointPath);

            foreach (BankDto bank in data) 
            {
                await _bankRepository.Add(bank);
            }

            return true;
        }
    }
}
