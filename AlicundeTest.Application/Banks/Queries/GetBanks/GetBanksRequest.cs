using AlicundeTest.Domain.Models;
using AlicundeTest.Domain.Shared;
using MediatR;

namespace AlicundeTest.Application.Banks.Queries.GetBanks;


/// <summary>
/// Class type request for MediatR
/// </summary>
public class GetBanksRequest : IRequest<ErrorOr<List<Bank>>>
{
}
