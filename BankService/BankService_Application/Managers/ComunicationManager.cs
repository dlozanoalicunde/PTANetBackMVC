namespace BankService_Application.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using BankService_Helper.DTO;

    public static class ComunicationManager
    {
        private static readonly HttpClient _client = new();

        public static void Init(string apiSource)
        {
            _client.BaseAddress = new Uri(apiSource);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<List<BankDto>> GetBanksData(string path)
        {
            List<BankDto> data = [];

            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsAsync<List<BankDto>>();
            }

            return data;
        }
    }
}
