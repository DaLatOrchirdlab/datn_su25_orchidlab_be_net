using orchid_backend_net.Domain.Entities;
using System.Diagnostics;
using Newtonsoft.Json;
using orchid_backend_net.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace orchid_backend_net.Infrastructure.Repository
{
    public class OrchidAnalyzerService(IConfiguration configuration) : IOrchidAnalyzerService
    {
        private static readonly SemaphoreSlim _pythonLock = new(1, 1);

        public async Task<OrchidAnalysisResult> AnalyzeAsync(byte[] imageBytes)
        {
            var pythonApiUrl = configuration["OrchidAnalyzer:PythonApiUrl"];
            if (string.IsNullOrEmpty(pythonApiUrl))
                throw new InvalidOperationException("OrchidAnalyzer:PythonApiUrl not configured");

            using var httpClient = new HttpClient();
            using var content = new MultipartFormDataContent
            {
                { new ByteArrayContent(imageBytes), "file", "image.jpg" }
            };

            var response = await httpClient.PostAsync(pythonApiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Python API failed: {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OrchidAnalysisResult>(json);
            return result ?? throw new Exception("Invalid JSON result from Python.");
        }
    }
}
