using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PTA.BL.Contracts;
using PTA.BL.Dtos;
using PTA.BL.Mapper;
using PTA.Infrastructure.DB;
using PTA.Models;
using System.Net;

namespace PTA.BL.Services
{
    public class MarketPartiesService(DataContext context, ILogger<MarketPartiesService> logger) : IMarketPartiesService
    {
        private readonly DataContext _context = context;
        private readonly ILogger<MarketPartiesService> _logger = logger;

        public async ValueTask<HttpStatusCode> CreateAsync(DistributionSystemOperatorDto dto)
        {
            try
            {
                var entity = DistributionSystemOperatorMapper.ToEntity(dto);
                entity.CreatedBy = "SYSTEM";
                entity.CreatedAt = DateTime.Now;

                await _context.DistributionSystemOperator.AddAsync(entity);
                await _context.SaveChangesAsync();

                return await Task.FromResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Create - An unexpected error occurred: {ex.Message}");
                throw new Exception($"Create - An unexpected error occurred: {ex.Message}");
            }
        }

        public async ValueTask DeleteAsync(int key)
        {
            var entity = await _context.DistributionSystemOperator.FindAsync(key);

            if (entity != null)
            {
                _context.DistributionSystemOperator.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async ValueTask<DistributionSystemOperatorDto> GetDistributionSystemOperatorByIdAsync(int id)
        {
            try
            {
                return DistributionSystemOperatorMapper.ToDto(await _context.DistributionSystemOperator
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GetDistributionSystemOperatorByIdAsync - An unexpected error occurred: {ex.Message}");
                throw new Exception($"GetDistributionSystemOperatorByIdAsync - An unexpected error occurred: {ex.Message}");
            }
        }

        public async ValueTask<List<DistributionSystemOperatorDto>> GetDistributionSystemOperatorsAsync()
        {
            try
            {
                return await _context.DistributionSystemOperator
                    .AsNoTracking()
                    .Select(e => DistributionSystemOperatorMapper.ToDto(e))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GetDistributionSystemOperatorsAsync - An unexpected error occurred: {ex.Message}");
                throw new Exception($"GetDistributionSystemOperatorsAsync - An unexpected error occurred: {ex.Message}");
            }
        }

        public async ValueTask LoadAsync(List<DistributionSystemOperatorDto> dtos)
        {
            try
            {
                var entities = new List<DistributionSystemOperator>();
                dtos.ForEach(dto =>
                {
                    var entity = DistributionSystemOperatorMapper.ToEntity(dto);
                    entity.CreatedBy = "SYSTEM";
                    entity.CreatedAt = DateTime.Now;

                    entities.Add(entity);
                });

                await _context.DistributionSystemOperator.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"LoadAsync - An unexpected error occurred: {ex.Message}");
                throw new Exception($"LoadAsync - An unexpected error occurred: {ex.Message}");
            }
        }

        public async ValueTask UpdateAsync(int key, DistributionSystemOperatorDto dto)
        {
            var entity = await _context.DistributionSystemOperator.FindAsync(key);
            var entityUdp = DistributionSystemOperatorMapper.ToEntity(dto);

            if (entity != null && entityUdp != null)
            {
                entity.CodingScheme = entityUdp.CodingScheme;
                entity.Country = entityUdp.Country;
                entity.DsoCode = entityUdp.DsoCode;
                entity.DsoName = entityUdp.DsoName;
                entity.ModifiedBy = "UDP";
                entity.ModifiedAt = DateTime.Now;

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}