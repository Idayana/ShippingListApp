using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Helpers;
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
         Task<PagedList<TEntity>> Get(PaginationParams pagParams);
         Task<bool> SaveAll();
         Task<TEntity> UpdateAsync(TEntity obj);
         Task<bool> ExistsAsync(TEntity obj);
         IQueryable<TEntity> GetQueryable();
        Task<PagedList<TEntity>> Query(IQueryable<TEntity> q, PaginationParams pagParams);
        IQueryable<TEntity> Include(IQueryable<TEntity> q, string relation);
    }
}