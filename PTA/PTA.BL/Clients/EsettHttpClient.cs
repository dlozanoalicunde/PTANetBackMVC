using PTA.BL.Contracts;
using PTA.BL.Dtos;
using System.Net.Http.Json;

namespace PTA.BL.Clients
{
    public class EsettHttpClient : IEsettHttpClient
    {
        private readonly HttpClient _httpClient;

        public EsettHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.opendata.esett.com/");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
        }

        public async ValueTask<List<DistributionSystemOperatorDto>> GetDistributionSystemOperatorsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<DistributionSystemOperatorDto>>("/EXP01/DistributionSystemOperators");
        }
    }
}