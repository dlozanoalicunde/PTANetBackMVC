/************************************************************************************************************
 * Julio's tech test for Alicunde Job position
 ************************************************************************************************************/

namespace BankService_Application.Query
{
    using System.Threading;
    using System.Threading.Tasks;
    using BankService_Domain;
    using BankService_Helper.DTO;
    using MediatR;

    /// <summary>
    /// Request hanlder for bank query
    /// </summary>
    public class GetBankByIDQueryHandler : IRequestHandler<GetBankByIDQuery, BankDto?>
    {
        private readonly IBankRepository _bankRepository;

        /// <summary>
        /// Principal constuctor
        /// </summary>
        /// <param name="bankRepository"></param>
        public GetBankByIDQueryHandler(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        /// <summary>
        /// Request handler method to resolve get bank by his identifcation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<BankDto?> Handle(GetBankByIDQuery request, CancellationToken cancellationToken)
        {
            return await _bankRepository.GetBy(request.Id);
        }
    }
}
