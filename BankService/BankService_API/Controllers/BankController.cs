/************************************************************************************************************
 * Julio's tech test for Alicunde Job position
 ************************************************************************************************************/

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
        private readonly ILogger _logger;

        /// <summary>
        /// Primary constructor
        /// </summary>
        /// <param name="mediator"></param>
        public BankController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
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
            try
            {
                BankDto? result = await _mediator.Send(new GetBankByIDQuery(id));

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on GetBy method, {ex.Message}", DateTime.UtcNow.ToLongTimeString());

                return BadRequest("Unable to execute your action, please try again later");
            }
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
            try
            {
                bool result = await _mediator.Send(new AddBankCommand(data));

                return Created(string.Empty, result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on Add method, {ex.Message}", DateTime.UtcNow.ToLongTimeString());

                return BadRequest("Unable to execute your action, please try again later");
            }
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
            try
            {
                bool result = await _mediator.Send(new PopulateBanksCommand(
                                                        "https://api.opendata.esett.com",
                                                        "EXP06/Banks"));

                return Created(string.Empty, result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on Populate method, {ex.Message}", 
                                 DateTime.UtcNow.ToLongTimeString());

                return BadRequest("Unable to execute your action, please try again later");
            }
        }
    }
}
