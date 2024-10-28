
namespace _2___Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("No se ha encontrado el elemento en la base de datos.") { }
        public NotFoundException(string message) : base(message) { }
    }
}
