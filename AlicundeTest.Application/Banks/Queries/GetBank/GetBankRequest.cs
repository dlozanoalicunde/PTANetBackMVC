using AlicundeTest.Domain.Models;
using AlicundeTest.Domain.Shared;
using MediatR;

namespace AlicundeTest.Application.Banks.Queries.GetBank;


/// <summary>
/// Record type request for MediatR
/// </summary>
/// <param name="Id">Bank unique identifier</param>
public sealed record GetBankRequest(Guid Id) : IRequest<ErrorOr<Bank>>;