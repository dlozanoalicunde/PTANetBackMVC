/************************************************************************************************************
 * Julio's tech test for Alicunde Job position
 ************************************************************************************************************/


namespace BankService_Infrastructure
{
    using BankService_Domain.Models;
    using BankService_Helper.DTO;

    /// <summary>
    /// Mapper internal static class to convert objects
    /// </summary>
    internal static class BankMapper
    {
        /// <summary>
        /// Convert entity into DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert DTO into entity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
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
