using Microsoft.AspNetCore.Mvc;
using MBAOptionsManager.Model;
using System.Text.Json;

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

        //[HttpGet(Name = "GetMbaOptionById")]
        //public ActionResult<MBAOption> GetMbaOption(int id)
        //{
        //    MBAOption? result = default;
        //    try
        //    {
        //        result = _context.MBAOptions.Where(x => x.Id == id).First();
        //    }
        //    catch (InvalidOperationException)
        //    {
        //        return NotFound();
        //    }
        //    return result;
        //}
    }
}