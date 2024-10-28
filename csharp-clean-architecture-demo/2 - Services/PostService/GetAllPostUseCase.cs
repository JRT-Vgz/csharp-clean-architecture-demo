
using _1___Entities;
using _2___Services.Interfaces;

namespace _2___Services.PostService
{
    public class GetAllPostUseCase
    {
        private readonly IExternalServiceAdapter<PostEntity> _adapter;
        public GetAllPostUseCase(IExternalServiceAdapter<PostEntity> adapter)
        {
            _adapter = adapter;
        }

        public async Task<IEnumerable<PostEntity>> ExecuteAsync()
            => await _adapter.GetAllExternalDataAsync();
    }
}
