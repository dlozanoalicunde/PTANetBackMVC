using AlicundeTest.Domain.Models;
using AlicundeTest.Domain.Shared;
using MediatR;

namespace AlicundeTest.Application.Banks.Queries.GetBank;

public sealed record GetBankRequest(Guid Id) : IRequest<ErrorOr<Bank>>;