using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.Extensions.Persistence.Repositories.Interface
{
    public interface IBaseRepository<TModel> where TModel : class
    {
        Task<TModel> GetAsync(Guid id);
        Task<IEnumerable<TModel>> GetAllAsync(Func<IQueryable<TModel>, IQueryable<TModel>> func = null);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task AddAsync(TModel entity);
        Task AddRangeAsync(List<TModel> models);
        Task UpdateAsync(TModel entity);
        Task RemoveByIdAsync(Guid id);
    }
}
