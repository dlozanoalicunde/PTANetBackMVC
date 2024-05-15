namespace BankService_Domain
{
    using BankService_Helper.DTO;

    public interface IBankRepository
    {
        Task<BankDto?> GetBy(int bankId);

        Task Add(BankDto bank);
    }
}
