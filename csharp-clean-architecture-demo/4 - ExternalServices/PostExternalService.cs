
using _3____Adapters.Interfaces;
using _3___Mappers.Dtos.AdaptersDtos;
using System.Text.Json;

namespace _4___ExternalServices
{
    public class PostExternalService : IExternalService<PostESDto>
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public PostExternalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<IEnumerable<PostESDto>> GetAllContentAsync()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<PostESDto>>(content, _jsonSerializerOptions);
        }

        public async Task<PostESDto> GetContentByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PostESDto>(content, _jsonSerializerOptions);
        }
    }
}
