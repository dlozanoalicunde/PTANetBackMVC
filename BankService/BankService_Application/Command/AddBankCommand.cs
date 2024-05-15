namespace BankService_Application.Command
{
    using BankService_Helper.DTO;
    using MediatR;

    public class AddBankCommand : IRequest<bool>
    {
        public BankDto Bank { get; }

        public AddBankCommand(BankDto bank) 
        {
            Bank = bank;
        }
    }
}
