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
        private static readonly string[] Countries = new[]
        {
            "Spain", "France", "Germany", "United Kingdom", "Portugal", "Italy"
        };
        private static readonly string[] CountryCodes = new[]
        {
            "ES", "FR", "DE", "GB", "PT", "IT"
        };
        private static readonly string pool = "abcdefghijklmnopqrstuvwxyz0123456789";

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
            Random.Shared.Next(0, Countries.Length);
            return Enumerable.Range(0, 5).Select(index => new MBAOption
            {
                Country = Countries[index],
                CountryCode = CountryCodes[index],
                MBAs = Enumerable.Range(1, Random.Shared.Next(2, 5)).Select(index => new MBA
                {
                    Code = Guid.NewGuid().ToString(),
                    Name = new string(Enumerable.Range(0, 10).Select(x => pool[Random.Shared.Next(0, pool.Length)]).ToArray())
                }).ToList()
            }).ToArray();
        }

        // GET: api/MBAOptions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MBAOption>> GetMBAOption(int id)
        {
            var mbaOption = await _context.MBAOptions.Include(m => m.MBAs).FirstOrDefaultAsync(m => m.Id == id);

            if (mbaOption == null)
            {
                return NotFound();
            }

            var mbaOptionDto = new MBAOption
            {
                Id = mbaOption.Id,
                Country = mbaOption.Country,
                CountryCode = mbaOption.CountryCode,
                MBAs = mbaOption.MBAs.Select(m => new MBA
                {
                    Id = m.Id,
                    Code = m.Code,
                    Name = m.Name
                }).ToList()
            };

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

            return Ok("Data imported successfully.");
        }
    }
}