/************************************************************************************************************
 * Julio's tech test for Alicunde Job position
 ************************************************************************************************************/


namespace BankService_Infrastructure.Repositories
{
    using System.Threading.Tasks;
    using BankService_Domain;
    using BankService_Domain.Models;
    using BankService_Helper.DTO;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Bank repository class responsable on CRUD methods
    /// </summary>
    public class BankRepository : IBankRepository
    {
        private readonly BankservicedbMdfContext _dbContext = new();

        /// <summary>
        /// Primary constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public BankRepository()
        {
        }

        /// <summary>
        /// Insert new bank into DB
        /// </summary>
        /// <param name="bank"></param>
        /// <returns></returns>
        public async Task Add(BankDto bank)
        {
            await _dbContext.Banks.AddAsync(BankMapper.ToEntity(bank));
            _dbContext .SaveChanges();
        }

        /// <summary>
        /// Select bank by his identification
        /// </summary>
        /// <param name="bankId"></param>
        /// <returns></returns>
        public async Task<BankDto?> GetBy(int bankId)
        {
            Bank entity = await _dbContext.Banks.SingleAsync(b => b.Id == bankId);

            return entity is not null ? BankMapper.ToDto(entity) : null;
        }
    }
}
