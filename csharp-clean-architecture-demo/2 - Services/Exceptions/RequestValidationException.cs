
namespace _2___Services.Exceptions
{
    public class RequestValidationException : Exception
    {
        public List<string> Errors { get; set; }
        public RequestValidationException() : base("Error de validación en la petición.") { }
        public RequestValidationException(string message)
        {
            Errors = new List<string>();
            Errors.Add(message);
        }
        public RequestValidationException(List<string> errors) 
        {
            Errors = errors;
        }
    }
}
