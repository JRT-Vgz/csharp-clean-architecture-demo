
using _1___Entities;
using _2___Services._Interfaces;
using System.Linq.Expressions;
using System;

namespace _2___Services.SaleService
{
    public class SearchAllSaleUseCase<TModel>
    {
        private IRepositorySearch<TModel, SaleEntity> _saleRepositorySearch;
        public SearchAllSaleUseCase(IRepositorySearch<TModel, SaleEntity> saleRepositorySearch)
        {
            _saleRepositorySearch = saleRepositorySearch;
        }

        public async Task<IEnumerable<SaleEntity>> ExecuteAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await _saleRepositorySearch.SearchAllAsync(predicate);
        }
    }
}
