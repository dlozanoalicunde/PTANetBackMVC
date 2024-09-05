using System.Net;
using BankAPI.DBO;
using BankAPI.Models;

namespace BankAPI.Services
{
    
    public class BankConsumer
    {

        private readonly HttpClient _httpClient;
        private readonly DataBaseConnection _dbConnection;
        private readonly ILogger<BankConsumer> _log;
        private const string apiToConsume = "https://api.opendata.esett.com/EXP06/Banks";

        //Constructor base for the consumer.
        public BankConsumer(DataBaseConnection connection)
        {
            _httpClient = new HttpClient();
            _dbConnection = connection;
            _log = LoggerFactory.Create(options => {}).CreateLogger<BankConsumer>();
        }

        //HTTP task that returns all the banks fetched.
        public async Task<HttpResponseMessage> StoreAllBanksInDataBase()
        {
            try
            {
                List<BankModel>? banksList = await _httpClient.GetFromJsonAsync<List<BankModel>>(apiToConsume);

                if (banksList == null || banksList.Count == 0)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotModified);
                     _log.LogWarning("Something went wrong API or Banks not found.");
                    return response;

                }

                _dbConnection.Banks.AddRange(banksList);
                await _dbConnection.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _log.LogWarning("Something went wrong API or Banks not found.");
                return new HttpResponseMessage(HttpStatusCode.NotModified);

            }
        }
    }
}
