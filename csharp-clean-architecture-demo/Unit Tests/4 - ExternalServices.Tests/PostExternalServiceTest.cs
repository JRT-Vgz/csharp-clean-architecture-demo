
using _3___Mappers.Dtos.AdaptersDtos;
using Moq;
using Moq.Protected;
using System.Net;

namespace _4___ExternalServices.Tests
{
    public class PostExternalServiceTest
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly PostExternalService _postExternalService;

        public PostExternalServiceTest()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://PostExternalService.com")
            };

            _postExternalService = new PostExternalService(_httpClient);
        }

        private void SetupHttpMessageHandlerMock_Response(HttpStatusCode statusCode, string content)
        {
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(content)
                });
        }

        [Fact]
        public async Task GetAllContentAsync_ReturnPosts_WhenResponseIsSuccessful()
        {
            var expectedList = new List<PostESDto>
            {
                new PostESDto { Id = 1, Title = "Test Post", Body = "Test Body" },
                new PostESDto { Id = 2, Title = "Test Post 2", Body = "Test Body 2" }
            };

            var serviceContent = "[{\"Id\": 1, \"Title\": \"Test Post\", \"Body\": \"Test Body\"}," +
                                 "{\"Id\": 2, \"Title\": \"Test Post 2\", \"Body\": \"Test Body 2\"}]";
            SetupHttpMessageHandlerMock_Response(HttpStatusCode.OK, serviceContent);

            var actual = await _postExternalService.GetAllContentAsync();
            var actualList = actual.ToList();

            Assert.NotNull(actualList);
            Assert.IsType<List<PostESDto>>(actualList);
            for (var i = 0; i < expectedList.Count; i++)
            {
                Assert.Equal(expectedList[i].Id, actualList[i].Id);
                Assert.Equal(expectedList[i].Title, actualList[i].Title);
                Assert.Equal(expectedList[i].Body, actualList[i].Body);
            }
        }

        [Fact]
        public async Task GetAllContentAsync_ThrowException_WhenResponseIsNotSuccessful()
        {
            SetupHttpMessageHandlerMock_Response(HttpStatusCode.NotFound, "Not Found");

            var actualException = await Assert.ThrowsAsync<HttpRequestException>(
                () => _postExternalService.GetAllContentAsync());
            Assert.Contains("404 (Not Found)", actualException.Message);
        }

        [Fact]
        public async Task GetContentByIdAsync_ReturnPost_WhenResponseIsSuccessful()
        {
            var expected = new PostESDto { Id = 1, Title = "Test Post", Body = "Test Body" };

            var serviceContent = "{\"Id\": 1, \"Title\": \"Test Post\", \"Body\": \"Test Body\"}";
            SetupHttpMessageHandlerMock_Response(HttpStatusCode.OK, serviceContent);

            var actual = await _postExternalService.GetContentByIdAsync(1);

            Assert.NotNull(actual);
            Assert.IsType<PostESDto>(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Body, actual.Body);
        }

        public async Task GetContentByIdAsync_ThrowException_WhenResponseIsNotSuccessful()
        {
            var expected = new PostESDto { Id = 1, Title = "Test Post", Body = "Test Body" };

            SetupHttpMessageHandlerMock_Response(HttpStatusCode.NotFound, "Not Found");

            var actualException = await Assert.ThrowsAsync<HttpRequestException>(
                () => _postExternalService.GetContentByIdAsync(1));
            Assert.Contains("404 (Not Found)", actualException.Message);
        }
    }
}

