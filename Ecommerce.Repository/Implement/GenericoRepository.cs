using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Repository.Contracts;
using Ecommerce.Repository.DBContext;

namespace Ecommerce.Repository.Implement
{
    public class GenericoRepository<TModel> : IGenericoRepository<TModel> where TModel : class
    {

        private readonly DBEcommerceContext _dbcontext;

        public GenericoRepository(DBEcommerceContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IQueryable<TModel> GetFiltered(Expression<Func<TModel, bool>>? filtro = null)
        {
            IQueryable<TModel> query = (filtro == null) ? _dbcontext.Set<TModel>() : _dbcontext.Set<TModel>().Where(filtro);

            return query;
        }

        public async Task<TModel> CreateAsync(TModel modelo)
        {
            try
            {
                _dbcontext.Set<TModel>().Add(modelo);
                await _dbcontext.SaveChangesAsync();
                return modelo;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TModel modelo)
        {
            try
            {
                _dbcontext.Set<TModel>().Update(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TModel modelo)
        {
            try
            {
                _dbcontext.Set<TModel>().Remove(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

    }
}