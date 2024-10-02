using StoreWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity>GetAsync(TKey id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);





    }
}
