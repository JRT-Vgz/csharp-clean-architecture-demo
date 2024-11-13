
using _1___Entities;
using _2___Services._Interfaces;
using _3____Adapters.Interfaces;
using _3___Mappers.Dtos.AdaptersDtos;
using AutoMapper;

namespace _3____Adapters
{
    public class PostESAdapter : IExternalServiceAdapter<PostEntity>
    {
        private readonly IExternalService<PostESDto> _externalService;
        private readonly IMapper _mapper;
        public PostESAdapter(IExternalService<PostESDto> externalService,
            IMapper mapper)
        {
            _externalService = externalService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostEntity>> GetAllExternalDataAsync()
        {
            var postESDtos = await _externalService.GetAllContentAsync();

            return postESDtos.Select(p => _mapper.Map<PostEntity>(p));
        }

        public async Task<PostEntity> GetDataByIdAsync(int id)
        {
            var postESDto = await _externalService.GetContentByIdAsync(id);

            return _mapper.Map<PostEntity>(postESDto);
        }
    }
}
