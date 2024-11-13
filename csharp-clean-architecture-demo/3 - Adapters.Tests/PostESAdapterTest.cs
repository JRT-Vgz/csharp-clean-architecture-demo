
using _1___Entities;
using _3____Adapters;
using _3____Adapters.Dtos;
using _3____Adapters.Interfaces;
using _3___Mappers.AutoMappers;
using AutoMapper;
using Moq;

namespace _3___Adapters.Tests
{
    public class PostESAdapterTest
    {
        private readonly Mock<IExternalService<PostESDto>> _externalServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PostESAdapter _adapter;

        public PostESAdapterTest()
        {
            _externalServiceMock = new Mock<IExternalService<PostESDto>>();
            _mapperMock = new Mock<IMapper>();
            _adapter = new PostESAdapter(_externalServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ReturnPostEntityList_Correctly()
        {
            var postESDtos = new List<PostESDto>()
            {
                new PostESDto(),
                new PostESDto(),
                new PostESDto()
            };

            _externalServiceMock.Setup(s => s.GetAllContentAsync()).ReturnsAsync(postESDtos);
            _mapperMock.Setup(m => m.Map<PostEntity>(It.IsAny<PostESDto>())).Returns(new PostEntity());

            var actual = await _adapter.GetAllExternalDataAsync();
            var actualList = actual.ToList();

            Assert.NotNull(actualList);
            Assert.IsType<List<PostEntity>>(actualList);
            Assert.Equal(postESDtos.Count, actualList.Count);

            _externalServiceMock.Verify(s => s.GetAllContentAsync(), Times.Once());
            _mapperMock.Verify(m => m.Map<PostEntity>(It.IsAny<PostESDto>()), Times.Exactly(3));
        }

        public async Task ReturnPostEntityById_Correctly()
        {
            _externalServiceMock.Setup(s => s.GetContentByIdAsync(It.IsAny<int>())).ReturnsAsync(new PostESDto());
            _mapperMock.Setup(m => m.Map<PostEntity>(It.IsAny<PostESDto>())).Returns(new PostEntity());

            var actual = await _adapter.GetDataByIdAsync(It.IsAny<int>());

            Assert.NotNull(actual);
            Assert.IsType<PostEntity>(actual);

            _externalServiceMock.Verify(s => s.GetContentByIdAsync(It.IsAny<int>()), Times.Once());
            _mapperMock.Verify(m => m.Map<PostEntity>(It.IsAny<PostESDto>()), Times.Once());
        }
    }  
}
