namespace MBAOptionsManager.Utilities
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient _client;

        public HttpClientFactory(HttpClient client)
        {
            _client = client;
        }

        public HttpClient CreateClient(string name)
        {
            return _client;
        }
    }
}
