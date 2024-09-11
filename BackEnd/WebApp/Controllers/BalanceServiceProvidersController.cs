using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Transactions;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceServiceProvidersController : ControllerBase
    {
        private readonly string connectionString = string.Empty;
        private readonly IHandleServiceProviders _mapping;

        public BalanceServiceProvidersController( IConfiguration configuration, IHandleServiceProviders mapping)
        {
            _mapping = mapping;
            connectionString = configuration.GetConnectionString("EndpointEsett");
        }


        // GET: api/BalanceServiceProviders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BalanceServiceProviders>> GetBalanceServiceProviders(Guid id)
        {
            try
            {
                return _mapping.BalanceServiceProvidersById(id);
            }
            catch (Exception)
            {

                throw new Exception("An error has occurred with your request");
            }
           
        }


        [HttpPut]
        public async Task<IActionResult> PutBalanceServiceProviders(BalanceServiceProviders balance)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _mapping.UpdateBalanceServiceProvidersById(balance);
                    scope.Complete();
                    return Ok("Transaction executed successfully");
                }
                catch (Exception)
                {

                    throw new Exception("An error has occurred with your request");
                }
            }
            

        }

        // POST: api/BalanceServiceProviders
        [HttpPost]
        public async Task<ActionResult<List<BalanceServiceProviders>>> PostBalanceServiceProviders()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                List<BalanceServiceProviders> dataResult = new List<BalanceServiceProviders>();
                try
                {
                    HttpClient client = new HttpClient();


                    var httpReponse = await client.GetAsync(connectionString + "?country=FI");
                    var statusCode = httpReponse.StatusCode == HttpStatusCode.OK ? 200 : 400;

                    if (statusCode == 200)
                    {
                        var responseBody = await httpReponse.Content.ReadAsStringAsync();

                        dataResult = JsonConvert.DeserializeObject<List<BalanceServiceProviders>>(responseBody);

                        foreach (var item in dataResult)
                        {

                            _mapping.BalanceServiceProviders(item);

                        }

                    }
                    else
                    {
                        return BadRequest();
                    }

                    scope.Complete();
                    return Ok(dataResult);
                }
                catch (Exception)
                {
                    scope.Dispose();
                    throw new Exception("An error has occurred with your request");
                }


            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBalanceServiceProviders(Guid id)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                try
                {
                    _mapping.DeleteBalanceServiceProvidersById(id);
                    scope.Complete();
                    return Ok("Transaction executed successfully");
                }
                catch (Exception)
                {

                    scope.Dispose();
                    throw new Exception("An error has occurred with your request");
                }


            }
        }

       
    }
}
