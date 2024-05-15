namespace BankService_API.Controllers
{
    using System.Net;
    using BankService_Application.Command;
    using BankService_Application.Query;
    using BankService_Helper.DTO;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/Banks")]
    public class BankController : Controller
    {
        private readonly IMediator _mediator;

        public BankController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{bankId}")]
        [ProducesResponseType(typeof(BankDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBy([FromRoute]int id)
        {
            BankDto? result = await _mediator.Send(new GetBankByIDQuery(id));

            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Add([FromBody] BankDto data)
        {
            bool result = await _mediator.Send(new AddBankCommand(data));

            return Created(string.Empty, result);
        }

        [HttpPost]
        [Route("populate")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Populate()
        {
            bool result = await _mediator.Send(new PopulateBanksCommand(
                                                        "https://api.opendata.esett.com", 
                                                        "EXP06/Banks"));

            return Created(string.Empty, result);
        }
    }
}
