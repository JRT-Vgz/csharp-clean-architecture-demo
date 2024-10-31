namespace _2___Services._Exceptions
{
    public class RequestValidationException : Exception
    {
        public List<string> Errors { get; set; }
        public RequestValidationException() : base("Error de validaci�n en la petici�n.") { }
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
