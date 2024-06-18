using AlicundeTest.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlicundeTest.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BanksController: ControllerBase
{
    protected readonly ILogger<BanksController> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger">Instancia de log</param>
    public BanksController(ILogger<BanksController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets a bank by id.
    /// </summary>
    /// <param name="id">Unique bank identifier</param>
    /// <returns>Bank</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Bank))]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok();
    }

    /// <summary>
    /// Gets all banks
    /// </summary>
    /// <returns>List of banks</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Bank>))]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }
}
