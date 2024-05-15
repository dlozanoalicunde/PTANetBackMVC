namespace BankService_Application.Command
{
    using System.Threading;
    using System.Threading.Tasks;
    using BankService_Domain;
    using MediatR;

    /// <summary>
    /// Add bank request handler class to insert banks
    /// </summary>
    public class AddBankCommandHandler : IRequestHandler<AddBankCommand, bool>
    {
        private readonly IBankRepository _bankRepository;

        /// <summary>
        /// Primary constructor
        /// </summary>
        /// <param name="bankRepository"></param>
        public AddBankCommandHandler(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        /// <summary>
        /// Request handler method to resolve insert business logic
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(AddBankCommand request, CancellationToken cancellationToken)
        {
            await _bankRepository.Add(request.Bank);

            return true;
        }
    }
}
