using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Models;

namespace ShoppingListApi.Interfaces
{
    public interface IBaseRepository <TEntity> : IDisposable where TEntity: Entity
    {
         DbSet<TEntity> entity { get; }
         Task<TEntity> AddAsync(TEntity obj);
         void Remove(int id);
         Task RemoveAsync(TEntity obj);
         Task<TEntity> Get(int id);
         Task<List<TEntity>> Get();
         Task<bool> SaveAll();
         Task<TEntity> UpdateAsync(TEntity obj);
         Task<bool> ExistsAsync(TEntity obj);
    }
}