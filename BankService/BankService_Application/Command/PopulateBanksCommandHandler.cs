namespace BankService_Application.Command
{
    using System.Threading;
    using System.Threading.Tasks;
    using BankService_Application.Managers;
    using BankService_Domain;
    using BankService_Helper.DTO;
    using MediatR;

    public class PopulateBanksCommandHandler : IRequestHandler<PopulateBanksCommand, bool>
    {
        private readonly IBankRepository _bankRepository;

        public PopulateBanksCommandHandler(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

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
