using Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service
{
    public class BaseService<TEntity> where TEntity : class, new()
    {
        protected BaseRepository<TEntity> baseRepository;

        public async Task<bool> InserItem(TEntity entity)
        {
            return await baseRepository.InsertItem(entity);
        }
        public async Task<bool> DeleteItem(Expression<Func<TEntity, bool>> func)
        {
            return await baseRepository.DeleteItem(func);
        }
        public async Task<bool> DeleteItem(int id)
        {
            return await baseRepository.DeleteItem(id);
        }

        public async Task<TEntity> FindItem(Expression<Func<TEntity, bool>> func)
        {
            return await baseRepository.FindItem(func);
        }

        public async Task<List<TEntity>> FindItemList()
        {
            return await baseRepository.FindItemList();
        }

        public async Task<List<TEntity>> FindItemList(Expression<Func<TEntity, bool>> func)
        {
            return await baseRepository.FindItemList(func);
        }
    }
}
