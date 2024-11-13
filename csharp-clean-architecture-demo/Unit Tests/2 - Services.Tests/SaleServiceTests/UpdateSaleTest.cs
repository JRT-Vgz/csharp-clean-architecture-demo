
using _2___Services.Services.SaleService;

namespace _2___Services.Tests.SaleServiceTests
{
    public class UpdateSaleTest
    {
        private readonly UpdateSaleUseCase _updateSaleUseCase;

        public UpdateSaleTest()
            => _updateSaleUseCase = new UpdateSaleUseCase();

        [Fact]
        public void ThrowException_IfCalled()
        {
            var actualException = Assert.Throws<NotImplementedException>(
                () => _updateSaleUseCase.Execute());
            Assert.Contains("No se puede modificar una venta directamente.", actualException.Message);
        }
    }
}
