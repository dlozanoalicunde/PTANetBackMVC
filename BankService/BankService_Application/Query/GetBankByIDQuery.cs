namespace BankService_Application.Query
{
    using BankService_Helper.DTO;
    using MediatR;

    public class GetBankByIDQuery : IRequest<BankDto?>
    {
        public int Id { get; }

        public GetBankByIDQuery(int id)
        { 
            Id = id;
        }
    }
}
