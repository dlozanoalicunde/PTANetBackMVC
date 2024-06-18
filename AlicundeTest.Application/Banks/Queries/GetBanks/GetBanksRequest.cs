using AlicundeTest.Domain.Models;
using MediatR;

namespace AlicundeTest.Application.Banks.Queries.GetBanks;

public class GetBanksRequest : IRequest<List<Bank>>
{
}
