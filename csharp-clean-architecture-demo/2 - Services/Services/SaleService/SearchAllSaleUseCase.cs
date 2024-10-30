
using _1___Entities;
using _2___Services._Interfaces;
using System.Linq.Expressions;
using System;
using AutoMapper;

namespace _2___Services.Services.SaleService
{
    public class SearchAllSaleUseCase<TModel, TDto>
    {
        private readonly IRepositorySearch<TModel, SaleEntity> _saleRepositorySearch;
        private readonly IMapper _mapper;
        public SearchAllSaleUseCase(IRepositorySearch<TModel, SaleEntity> saleRepositorySearch,
            IMapper mapper)
        {
            _saleRepositorySearch = saleRepositorySearch;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> ExecuteAsync(Expression<Func<TModel, bool>> predicate)
        {
            var saleEntities = await _saleRepositorySearch.SearchAllAsync(predicate);

            return saleEntities.Select(s => _mapper.Map<TDto>(s));
        }
    }
}
