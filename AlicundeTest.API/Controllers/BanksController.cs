using MediatR;
using AlicundeTest.Domain.Models;
using AlicundeTest.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using AlicundeTest.Application.Banks.Queries.GetBank;
using AlicundeTest.Application.Banks.Queries.GetBanks;

namespace AlicundeTest.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BanksController: ControllerBase
{
    protected readonly ILogger<BanksController> _logger;
    protected readonly IMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger">Instancia de log</param>
    public BanksController(ILogger<BanksController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a bank by id.
    /// </summary>
    /// <param name="id">Unique bank identifier</param>
    /// <returns>Bank</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ErrorOr<Bank>))]
    public async Task<IActionResult> Get(Guid id)
    {
        var resul = await _mediator.Send(new GetBankRequest(id));
        return Ok(resul);
    }

    /// <summary>
    /// Gets all banks
    /// </summary>
    /// <returns>List of banks</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ErrorOr<List<Bank>>))]
    public async Task<IActionResult> GetAll()
    {
        var resul = await _mediator.Send(new GetBanksRequest());
        return Ok(resul);
    }
}
