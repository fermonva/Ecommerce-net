using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Contracts
{
    /// <summary>
    /// Defines the contract for a generic repository.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IGenericoRepository<TModel> where TModel : class
    {
        IQueryable<TModel> GetFiltered(Expression<Func<TModel, bool>>? filtro = null);

        Task<TModel> CreateAsync(TModel modelo);

        Task<bool> UpdateAsync(TModel modelo);

        Task<bool> DeleteAsync(TModel modelo);
    }
}