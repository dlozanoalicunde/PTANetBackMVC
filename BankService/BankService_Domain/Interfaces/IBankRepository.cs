/************************************************************************************************************
 * Julio's tech test for Alicunde Job position
 ************************************************************************************************************/

namespace BankService_Domain
{
    using BankService_Helper.DTO;

    /// <summary>
    /// Bank repository interface
    /// </summary>
    public interface IBankRepository
    {
        Task<BankDto?> GetBy(int bankId);

        Task Add(BankDto bank);
    }
}
