using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Neo.Extensions.Persistence.Repositories.Base
{
    public abstract class BaseRepository<TModel> : Interface.IBaseRepository<TModel> where TModel : class
    {
        protected readonly DbContext DatabaseContext;

        public BaseRepository(DbContext context)
        {
            DatabaseContext = context;
        }

        public async Task AddAsync(TModel entity)
        {
            await DatabaseContext.Set<TModel>().AddAsync(entity);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<TModel> models)
        {
            await DatabaseContext.Set<TModel>().AddRangeAsync(models);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task<TModel> GetAsync(Guid id)
        {
            return await DatabaseContext.Set<TModel>().FindAsync(id);
        }

        public async Task UpdateAsync(TModel entity)
        {
            DatabaseContext.Set<TModel>().Update(entity);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync(Func<IQueryable<TModel>, IQueryable<TModel>> func = null)
        {
            var dbSet = DatabaseContext.Set<TModel>();

            if (func != null)
            {
                return await func(dbSet).ToListAsync();
            }

            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await DatabaseContext.Set<TModel>().ToListAsync();
        }

        public async Task RemoveByIdAsync(Guid id)
        {
            var existingEntity = DatabaseContext.Set<TModel>().FindAsync(id);
            DatabaseContext.Remove(existingEntity);
            await DatabaseContext.SaveChangesAsync();
        }
    }
}
