namespace BankService_Application.Query
{
    using BankService_Helper.DTO;
    using MediatR;

    /// <summary>
    /// Query request class for get bank by id
    /// </summary>
    public class GetBankByIDQuery : IRequest<BankDto?>
    {
        public int Id { get; }

        public GetBankByIDQuery(int id)
        { 
            Id = id;
        }
    }
}
