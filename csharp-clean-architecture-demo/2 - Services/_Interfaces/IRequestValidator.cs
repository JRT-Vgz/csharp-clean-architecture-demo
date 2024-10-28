
namespace _2___Services.Interfaces
{
    public interface IRequestValidator<TRequestDto>
    {
        public List<string> Errors { get; set; }
        Task<bool> Validate(TRequestDto requestDto);
    }
}
