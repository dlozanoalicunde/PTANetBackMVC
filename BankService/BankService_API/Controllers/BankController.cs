namespace BankService_API.Controllers
{
    using System.Net;
    using BankService_Application.Command;
    using BankService_Application.Query;
    using BankService_Helper.DTO;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Bank Api controller
    /// </summary>
    [ApiController]
    [Route("api/Banks")]
    public class BankController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Primary constructor
        /// </summary>
        /// <param name="mediator"></param>
        public BankController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Getter method to return bank by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{bankId}")]
        [ProducesResponseType(typeof(BankDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBy([FromRoute]int id)
        {
            BankDto? result = await _mediator.Send(new GetBankByIDQuery(id));

            return Ok(result);
        }

        /// <summary>
        /// Post method to insert a bank into DB
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Add([FromBody] BankDto data)
        {
            bool result = await _mediator.Send(new AddBankCommand(data));

            return Created(string.Empty, result);
        }

        /// <summary>
        /// Post method to populate bank Db from another web api
        /// </summary>
        /// <returns></returns>
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
