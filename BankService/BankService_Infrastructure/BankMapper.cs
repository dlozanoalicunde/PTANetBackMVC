namespace BankService_Infrastructure
{
    using BankService_Domain.Models;
    using BankService_Helper.DTO;

    internal static class BankMapper
    {
        public static BankDto ToDto(Bank entity)
        {
            BankDto dto = new()
            {
                Id = entity.Id,
                Bic = entity.Bic,
                Country = entity.Country,
                Name = entity.Name
            };

            return dto;
        }

        public static Bank ToEntity(BankDto dto)
        {
            Bank bank = new()
            {
                Bic = dto.Bic,
                Country = dto.Country,
                Name = dto.Name
            };

            return bank;
        }
    }
}
