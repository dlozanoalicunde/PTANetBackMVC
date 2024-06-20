using AlicundeTest.Domain.Models;
using AlicundeTest.Domain.Repositories;
using AlicundeTest.Domain.Shared;
using MediatR;

namespace AlicundeTest.Application.Banks.Queries.GetBanks;

public class GetBanksHandler : IRequestHandler<GetBanksRequest, ErrorOr<List<Bank>>>
{
    private readonly IBanksRepository _banksRepository;

    public GetBanksHandler(IBanksRepository banksRepository)
    {
        _banksRepository = banksRepository;
    }

    public async Task<ErrorOr<List<Bank>>> Handle(GetBanksRequest request, CancellationToken cancellationToken)
    {
        var bank = await _banksRepository.GetAll(cancellationToken);

        return ErrorOr<List<Bank>>.FromValue(bank.ToList());
    }
}
