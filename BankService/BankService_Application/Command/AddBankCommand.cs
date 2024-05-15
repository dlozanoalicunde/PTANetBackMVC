/************************************************************************************************************
 * Julio's tech test for Alicunde Job position
 ************************************************************************************************************/

namespace BankService_Application.Command
{
    using BankService_Helper.DTO;
    using MediatR;

    /// <summary>
    /// Add bank command request class
    /// </summary>
    public class AddBankCommand : IRequest<bool>
    {
        public BankDto Bank { get; }

        public AddBankCommand(BankDto bank) 
        {
            Bank = bank;
        }
    }
}
