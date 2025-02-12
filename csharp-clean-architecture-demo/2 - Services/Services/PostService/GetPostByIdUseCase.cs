
using _1___Entities;
using _2___Services._Interfaces;

namespace _2___Services.Services.PostService
{
    public class GetPostByIdUseCase
    {
        private readonly IExternalServiceAdapter<PostEntity> _adapter;
        public GetPostByIdUseCase(IExternalServiceAdapter<PostEntity> adapter)
        {
            _adapter = adapter;
        }

        public async Task<PostEntity> ExecuteAsync(int id)
            => await _adapter.GetDataByIdAsync(id);
    }
}
