using Microsoft.AspNetCore.Mvc;
using MBAOptionsManager.Model;

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

        public MBAOptionsController(ILogger<MBAOptionsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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
                }).ToArray()
            }).ToArray();
        }
    }
}