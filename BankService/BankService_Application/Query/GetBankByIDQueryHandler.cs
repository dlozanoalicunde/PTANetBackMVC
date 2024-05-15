namespace BankService_Application.Query
{
    using System.Threading;
    using System.Threading.Tasks;
    using BankService_Domain;
    using BankService_Helper.DTO;
    using MediatR;

    public class GetBankByIDQueryHandler : IRequestHandler<GetBankByIDQuery, BankDto?>
    {
        private readonly IBankRepository _bankRepository;

        public GetBankByIDQueryHandler(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<BankDto?> Handle(GetBankByIDQuery request, CancellationToken cancellationToken)
        {
            return await _bankRepository.GetBy(request.Id);
        }
    }
}
