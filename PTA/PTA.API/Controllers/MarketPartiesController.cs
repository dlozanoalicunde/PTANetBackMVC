using Microsoft.AspNetCore.Mvc;
using PTA.BL.Contracts;
using PTA.BL.Dtos;
using Swashbuckle.AspNetCore.Annotations;
using static System.Net.Mime.MediaTypeNames;

namespace PTA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketPartiesController(IEsettHttpClient esettHttpClient,
        IMarketPartiesService marketPartiesService,
        ILogger<MarketPartiesController> logger) : ControllerBase
    {
        private readonly IEsettHttpClient _esettHttpClient = esettHttpClient;
        private readonly IMarketPartiesService _marketPartiesService = marketPartiesService;
        private readonly ILogger<MarketPartiesController> _logger = logger;

        [HttpGet("GetDistributionSystemOperators"), SwaggerOperation(
            Summary = "Get a list with all Distribution System Operators from DB",
            Description = "Get a list with all Distribution System Operators from DB",
            OperationId = "GetDistributionSystemOperators",
            Tags = ["MarketParties"])]
        [Produces(Application.Json)]
        [ProducesResponseType(typeof(List<DistributionSystemOperatorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDistributionSystemOperators()
        {
            var distributionSystemOperators = await _marketPartiesService.GetDistributionSystemOperatorsAsync();

            return distributionSystemOperators is null ? NotFound() : new OkObjectResult(distributionSystemOperators);
        }

        [HttpGet("GetDistributionSystemOperatorByKey"), SwaggerOperation(
            Summary = "Get a Distribution System Operator by Id",
            Description = "Get a Distribution System Operator by Id",
            OperationId = "GetDistributionSystemOperatorByKey",
            Tags = ["MarketParties"])]
        [Produces(Application.Json)]
        [ProducesResponseType(typeof(List<DistributionSystemOperatorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDistributionSystemOperatorByKey(int id)
        {
            var distributionSystemOperator = await _marketPartiesService.GetDistributionSystemOperatorByIdAsync(id);

            return distributionSystemOperator is null ? NotFound() : new OkObjectResult(distributionSystemOperator);
        }

        [HttpPost("CreateDistributionSystemOperator"), SwaggerOperation(
            Summary = "Add a new Distribution System Operator object into DB",
            Description = "Add a new Distribution System Operator object into DB",
            OperationId = "CreateDistributionSystemOperator",
            Tags = ["MarketParties"])]
        [Produces(Application.Json)]
        [ProducesResponseType(typeof(DistributionSystemOperatorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDistributionSystemOperator(DistributionSystemOperatorDto dto)
        {
            if (dto == null || (string.IsNullOrWhiteSpace(dto.Country) && string.IsNullOrWhiteSpace(dto.CodingScheme) && string.IsNullOrWhiteSpace(dto.DsoCode) && string.IsNullOrWhiteSpace(dto.DsoName)))
            {
                return BadRequest();
            }

            var statusCode = await _marketPartiesService.CreateAsync(dto);

            return Ok();
        }

        [HttpPut("UpdateDistributionSystemOperator/{id}"), SwaggerOperation(
            Summary = "Update an existing DB object",
            Description = "Update an existing DB object",
            OperationId = "UpdateDistributionSystemOperator",
            Tags = ["MarketParties"])]
        [Produces(Application.Json)]
        [ProducesResponseType(typeof(DistributionSystemOperatorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDistributionSystemOperator(int id, DistributionSystemOperatorDto dto)
        {
            if (dto == null || (string.IsNullOrWhiteSpace(dto.Country) && string.IsNullOrWhiteSpace(dto.CodingScheme) && string.IsNullOrWhiteSpace(dto.DsoCode) && string.IsNullOrWhiteSpace(dto.DsoName)))
            {
                return BadRequest();
            }

            await _marketPartiesService.UpdateAsync(id, dto);

            return Ok();
        }

        [HttpDelete("DeleteDistributionSystemOperator/{id}"), SwaggerOperation(
            Summary = "Remove an existing DB object",
            Description = "Remove an existing DB object",
            OperationId = "DeleteDistributionSystemOperator",
            Tags = ["MarketParties"])]
        [Produces(Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDistributionSystemOperator(int id)
        {
            await _marketPartiesService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("GetExternalDistributionSystemOperators"), SwaggerOperation(
            Summary = "Get a list with all Distribution System Operators from Esett API",
            Description = "Get a list with all Distribution System Operators",
            OperationId = "GetExternalDistributionSystemOperators",
            Tags = ["MarketParties"])]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<DistributionSystemOperatorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetExternalDistributionSystemOperators()
        {
            var distributionSystemOperators = await _esettHttpClient.GetDistributionSystemOperatorsAsync();

            return distributionSystemOperators is null ? NotFound() : Ok(distributionSystemOperators);
        }

        [HttpGet("ImportExternalDistributionSystemOperators"), SwaggerOperation(
            Summary = "Get a list with all Distribution System Operators from Esett API and save the collection result into DB",
            Description = "Get a list with all Distribution System Operators from Esett API and save the collection result into DB",
            OperationId = "ImportExternalDistributionSystemOperators",
            Tags = ["MarketParties"])]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<DistributionSystemOperatorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ImportExternalDistributionSystemOperators()
        {
            var distributionSystemOperators = await _esettHttpClient.GetDistributionSystemOperatorsAsync();

            if (distributionSystemOperators is null) return NotFound();
            if (distributionSystemOperators.Count == 0) return NoContent();

            await _marketPartiesService.LoadAsync(distributionSystemOperators);
            return Ok();
        }
    }
}