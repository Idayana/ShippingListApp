using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using ShoppingListApi.Data;
using ShoppingListApi.Helpers;
using ShoppingListApi.Models;

namespace ShoppingListApi.Interfaces
{
    public abstract class BaseRepository <TEntity> : IBaseRepository<TEntity> where TEntity: Entity
    {
        protected bool disposedValue = false;
        private readonly DataContext _context;
        public DbSet<TEntity> entity { get; }
        public BaseRepository(DataContext context)
        {
            _context = context;
            entity = _context.Set<TEntity>();
        }
        public virtual async Task<TEntity> AddAsync(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "The given object must not be null.");
            }
            await entity.AddAsync(obj);
            return obj;
        }

        public void Remove(int id)
        {
            var objToDelete = entity.FirstOrDefault(ent => ent.Id.Equals(id));
            if (objToDelete == null)
            {
                throw new ArgumentException("Does not exist an element with the given key", nameof(id));
            }
            entity.Remove(objToDelete);
        }

        public async Task RemoveAsync (TEntity obj)
        {
            if(obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "The given object must not be null.");
            }

            await Task.Factory.StartNew(() =>
            {
                entity.Remove(obj);
            });
            
        }

        /// <summary>
        /// Include relations on the TEntity
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public abstract IQueryable<TEntity> IncludeGet(IQueryable<TEntity> entities);

        public async Task<TEntity> Get(int id)
        {
            var include = IncludeGet(GetQueryable());
            return await include.Where(t => t.Id == id).SingleAsync();
        }

        public IQueryable<TEntity> GetQueryable(){
            return entity.AsQueryable();
        }

        public async Task<PagedList<TEntity>> Query(IQueryable<TEntity> q, PaginationParams pagParams){
        
            return await PagedList<TEntity>.CreateAsync(q, pagParams.PageNumber, pagParams.PageSize);
            
        }
        
        public async Task<PagedList<TEntity>> Get(PaginationParams pagParams)
        {
            var ent= GetQueryable();
            return await PagedList<TEntity>.CreateAsync(ent, pagParams.PageNumber, pagParams.PageSize);
            //return await entity.AsQueryable().ToListAsync();
        }

        public IQueryable<TEntity> Include(IQueryable<TEntity> q, string relation){
            return q.Include(relation);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<TEntity> UpdateAsync(TEntity obj)
        {
            if(obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "The given object must not be null.");
            }

            bool exists= await this.ExistsAsync(obj);
            if(exists == false)
            {
                throw new ArgumentException("The given object does not exist.", nameof(obj));
            }

            await Task.Factory.StartNew(()=> 
            {
                entity.Update(obj);                    
            });

            return obj;
        }

        public virtual async Task<bool> ExistsAsync(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "The given object must not be null.");
            }

            return await entity.ContainsAsync(obj);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                this.disposedValue = true;
            }
        }
    }
}