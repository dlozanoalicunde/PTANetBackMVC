namespace BankService_Application.Command
{
    using System.Threading;
    using System.Threading.Tasks;
    using BankService_Domain;
    using MediatR;

    public class AddBankCommandHandler : IRequestHandler<AddBankCommand, bool>
    {
        private readonly IBankRepository _bankRepository;

        public AddBankCommandHandler(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<bool> Handle(AddBankCommand request, CancellationToken cancellationToken)
        {
            await _bankRepository.Add(request.Bank);

            return true;
        }
    }
}
