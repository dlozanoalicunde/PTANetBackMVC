using AlicundeTest.Domain.Models;
using AlicundeTest.Domain.Repositories;
using AlicundeTest.Domain.Shared;
using MediatR;

namespace AlicundeTest.Application.Banks.Queries.GetBank;

public class GetBankHandler : IRequestHandler<GetBankRequest, ErrorOr<Bank>>
{
    private readonly IBanksRepository _banksRepository;

    public GetBankHandler(IBanksRepository banksRepository)
    {
        _banksRepository = banksRepository;
    }

    public async Task<ErrorOr<Bank>> Handle(GetBankRequest request, CancellationToken cancellationToken)
    {
        var bank = await _banksRepository.GetBank(request.Id, cancellationToken);
        if (bank == null)
            return ErrorOr<Bank>.FromError($"Bank with ID {request.Id} not found");
        

        return ErrorOr<Bank>.FromValue(bank);
    }
}
