
namespace _2___Services.Services.SaleService
{
    public class UpdateSaleUseCase
    {
        public void Execute()
        {
            throw new NotImplementedException("No se puede modificar una venta directamente. Si se modifica o borra un concepto, esto modificar� autom�ticamente " +
                "su venta asociada. Si una venta se queda sin conceptos, ser� eliminada autom�ticamente.");
        }
    }
}
