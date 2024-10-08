﻿using StoreWeb.Core.Entities;
using StoreWeb.Core.Specifications;
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
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity,TKey> spec);

        Task<TEntity>GetWithSpecAsync(ISpecifications<TEntity, TKey> spec);

        Task AddAsync(TEntity entity);

        Task<int> CountAsync(ISpecifications<TEntity,TKey> spec);

        void Update(TEntity entity);

        void Delete(TEntity entity);





    }
}
