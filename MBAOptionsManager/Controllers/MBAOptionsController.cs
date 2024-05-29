using Microsoft.AspNetCore.Mvc;
using MBAOptionsManager.Model;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace MBAOptionsManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MBAOptionsController : ControllerBase
    {

        private readonly ILogger<MBAOptionsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public MBAOptionsController(ILogger<MBAOptionsController> logger, ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet(Name = "GetMBAOptions")]
        public IEnumerable<MBAOption> GetMbaOptions()
        {
            _logger.LogInformation("Queried MBA options");
            return _context.MBAOptions;
        }

        // GET: api/MBAOptions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ExternalMBAOption>> GetMBAOption(int id)
        {
            var mbaOption = await _context.MBAOptions.Include(m => m.MBAs).FirstOrDefaultAsync(m => m.Id == id);

            if (mbaOption == null)
            {
                return NotFound();
            }

            var mbaOptionDto = new ExternalMBAOption
            {
                Country = mbaOption.Country,
                CountryCode = mbaOption.CountryCode,
                MBAs = mbaOption.MBAs.Select(m => new ExternalMBA
                {
                    Code = m.Code,
                    Name = m.Name
                }).ToList()
            };
            _logger.LogInformation($"Queried MBA option with ID {mbaOption.Id}");

            return mbaOptionDto;
        }

        // POST: api/MBAOptions
        [HttpPost]
        public async Task<ActionResult<MBAOption>> CreateMBAOption(ExternalMBAOption createMBAOptionDto)
        {
            var mbaOption = new MBAOption
            {
                Country = createMBAOptionDto.Country,
                CountryCode = createMBAOptionDto.CountryCode,
                MBAs = createMBAOptionDto.MBAs.Select(m => new MBA
                {
                    Code = m.Code,
                    Name = m.Name
                }).ToList()
            };

            _context.MBAOptions.Add(mbaOption);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Created new MBA option with ID {mbaOption.Id}");

            return CreatedAtAction(nameof(GetMBAOption), new { id = mbaOption.Id }, mbaOption);
        }

        // PUT: api/MBAOptions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMBAOption(int id, ExternalMBAOption updateMBAOptionDto)
        {
            var mbaOption = await _context.MBAOptions.Include(m => m.MBAs).FirstOrDefaultAsync(m => m.Id == id);

            if (mbaOption == null)
            {
                return NotFound();
            }

            mbaOption.Country = updateMBAOptionDto.Country;
            mbaOption.CountryCode = updateMBAOptionDto.CountryCode;
            mbaOption.MBAs = updateMBAOptionDto.MBAs.Select(m => new MBA
            {
                Code = m.Code,
                Name = m.Name,
                MBAOptionId = mbaOption.Id
            }).ToList();

            _context.Entry(mbaOption).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            _logger.LogInformation($"Updated MBA option with ID {mbaOption.Id}");

            return NoContent();
        }

        // DELETE: api/MBAOptions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMBAOption(int id)
        {
            var mbaOption = await _context.MBAOptions.FindAsync(id);

            if (mbaOption == null)
            {
                return NotFound();
            }

            _context.MBAOptions.Remove(mbaOption);
            await _context.SaveChangesAsync();


            _logger.LogInformation($"Removed MBA option with ID {mbaOption.Id}");

            return NoContent();
        }

        [HttpPost("Import")]
        public async Task<IActionResult> ImportMBAOptions()
        {
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync("https://api.opendata.esett.com/EXP04/MBAOptions");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to fetch data from external API. Status code: {response.StatusCode}");
                return StatusCode((int)response.StatusCode, "Failed to fetch data from external API.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var externalMBAOptions = JsonSerializer.Deserialize<List<ExternalMBAOption>>(jsonResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var mbaOptions = externalMBAOptions.Select(option => new MBAOption
            {
                Country = option.Country,
                CountryCode = option.CountryCode,
                MBAs = option.MBAs.Select(mba => new MBA
                {
                    Code = mba.Code,
                    Name = mba.Name
                }).ToList()
            }).ToList();

            _context.MBAOptions.AddRange(mbaOptions);
            await _context.SaveChangesAsync();


            _logger.LogInformation($"Imported MBA options from external endpoint");

            return Ok("Data imported successfully.");
        }
    }
}