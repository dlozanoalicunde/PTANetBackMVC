using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAPI.DBO;
using BankAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly DataBaseConnection _context;
        public BankController(DataBaseConnection context)
        {
            _context = context;
        }
        // GET: api/<BankController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _context.Banks.ToListAsync();
            if(result==null)
                return NotFound();
            return Ok(result);
        }

        // Search by ID filter.
        // GET api/<BankController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult>  Get(int id)
        {
            var result = await _context.Banks.SingleOrDefaultAsync(x => x.BankID == id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST api/<BankController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BankModel bank)
        {
            await _context.Banks.AddAsync(bank);
            await _context.SaveChangesAsync();
            return Ok(bank);
        }

        // PUT api/<BankController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BankModel bank)
        {
            var bankInfo = _context.Banks.SingleOrDefault(x => x.BankID == id);
            if(bankInfo == null)
                return NotFound();

            bankInfo.Name = bank.Name;
            bankInfo.Bic = bank.Bic;
            bankInfo.Country = bank.Country;
            _context.Attach(bankInfo);
            await _context.SaveChangesAsync();

            return Ok(bankInfo);
        }

        // DELETE api/<BankController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bankInfo = _context.Banks.SingleOrDefault(x => x.BankID == id);
            if (bankInfo == null)
                return NotFound();
            _context.Banks.Remove(bankInfo);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
