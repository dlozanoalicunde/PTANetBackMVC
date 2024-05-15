namespace BankService_Infrastructure.Repositories
{
    using System.Threading.Tasks;
    using BankService_Domain;
    using BankService_Domain.Models;
    using BankService_Helper.DTO;
    using Microsoft.EntityFrameworkCore;

    public class BankRepository : IBankRepository
    {
        private readonly BankservicedbMdfContext _dbContext;

        public BankRepository(BankservicedbMdfContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Add(BankDto bank)
        {
            await _dbContext.Banks.AddAsync(BankMapper.ToEntity(bank));
        }

        public async Task<BankDto?> GetBy(int bankId)
        {
            Bank entity = await _dbContext.Banks.SingleAsync(b => b.Id == bankId);

            return entity is not null ? BankMapper.ToDto(entity) : null;
        }
    }
}
