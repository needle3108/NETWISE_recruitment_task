using Microsoft.Extensions.Logging;

namespace ConsoleApplication{
    public class ConnectService : IConnectService{
        private readonly ILogger<ConnectService> _logger;
        public ConnectService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ConnectService>();
        }

        public async Task<string> GetAsync(){
            using(var httpClient = new HttpClient(){
                BaseAddress = new Uri(StaticStrings.Uri)
                })
            {
                var response = await httpClient.GetAsync(StaticStrings.Endpoint);
                response.EnsureSuccessStatusCode();

                _logger.LogDebug("Getting a response");   

                return await response.Content.ReadAsStringAsync();
            } 
    
        }

    }
}