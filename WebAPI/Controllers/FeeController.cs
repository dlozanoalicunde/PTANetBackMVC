using Domain.Entities;
using Infrastructure.DTOs.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeeController : ControllerBase
    {
        private readonly FeeService _service;
        private readonly ILogger<FeeController> _logger;
        public FeeController(FeeService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> InsertFeeDataAsync()
        {
            BaseResponse<Fee> resp = await _service.PopulateFeeDbAsync();

            if (resp.Errors.Count > 0)
            {
                return StatusCode(500, resp);
            }

            return Ok(resp);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAsync([FromQuery] int? id, [FromQuery] int? page, [FromQuery] int? pageSize)
        {
            BaseResponse<Fee> resp = await _service.GetFeeAsync(id, page, pageSize);

            if (resp.Errors.Count > 0)
            {
                return StatusCode(500, resp);
            }

            return Ok(resp);
        }
    }
}
