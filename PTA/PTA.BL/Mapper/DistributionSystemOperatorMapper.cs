using PTA.BL.Dtos;
using PTA.Models;

namespace PTA.BL.Mapper
{
    public class DistributionSystemOperatorMapper
    {
        internal static DistributionSystemOperatorDto ToDto(DistributionSystemOperator entity)
        {
            if (entity != null)
            {
                return new DistributionSystemOperatorDto()
                {
                    CodingScheme = entity.CodingScheme,
                    Country = entity.Country,
                    DsoCode = entity.DsoCode,
                    DsoName = entity.DsoName
                };
            }

            return null;
        }

        internal static DistributionSystemOperator ToEntity(DistributionSystemOperatorDto dto)
        {
            if (dto != null)
            {
                return new DistributionSystemOperator()
                {
                    CodingScheme = dto.CodingScheme,
                    Country = dto.Country,
                    DsoCode = dto.DsoCode,
                    DsoName = dto.DsoName
                };
            }

            return null;
        }
    }
}