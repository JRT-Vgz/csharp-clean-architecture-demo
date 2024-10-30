
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2___Services._Exceptions
{
    public class EntityValidationException : Exception
    {
        public List<string> Errors { get; set; }
        public EntityValidationException() : base("Error de l�gica de negocio.") { }
        public EntityValidationException(string message)
        {
            Errors = new List<string>();
            Errors.Add(message);
        }
        public EntityValidationException(List<string> errors)
        {
            Errors = errors;
        }
    }
}
