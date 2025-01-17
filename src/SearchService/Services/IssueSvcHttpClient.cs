using SearchService.Models;

namespace SearchService.Services
{
    public class IssueSvcHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public IssueSvcHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<Issue>> GetIssuesForSearchDb()
        {
            return await _httpClient.GetFromJsonAsync<List<Issue>>(_configuration["IssueServiceUrl"] + "/api/issues");
        }
    }
}