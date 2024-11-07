using _1___Entities;
using _2___Services._Interfaces;
using _3___Mappers.Dtos.BrandDtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Services.Tests
{
    public class BrandServiceTest
    {
        private readonly IRepository<BrandEntity> _brandRepository;
        private readonly IMapper _mapper;

        public BrandServiceTest()
        {
            var mockBrandRepository = new Mock<IRepository<BrandEntity>>();
            mockBrandRepository
                .Setup(p => p.GetAllAsync())
                .ReturnsAsync(new List<BrandEntity>
                {
                    new BrandEntity { Id = 1, Name = "Marca1" },
                    new BrandEntity { Id = 2, Name = "Marca2" }
                });

            var mockMapper = new Mock<IMapper>();

            _brandRepository = mockBrandRepository.Object;
            _mapper = mockMapper.Object;
        }

        [Fact]
        public async Task GetAll_OK()
        {

            var brandEntities = await _brandRepository.GetAllAsync();

            Assert.IsAssignableFrom<IEnumerable<BrandEntity>>(brandEntities);
            Assert.True(brandEntities.Any());
            //var result = brandEntities.Select(b => _mapper.Map<TDto>(b));
        }
    }
}