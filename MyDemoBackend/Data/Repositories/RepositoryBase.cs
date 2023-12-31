﻿
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Model.Extensions;

namespace Data.Repositories
{


    #region TEntity and TKey

    #region TEntity and TKey

    internal class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class where TKey : struct
    {
        protected readonly DemoBackendDbContext Context;
        protected readonly DbSet<TEntity> DbSet;
        protected IQueryable<TEntity> Queryable;
        public RepositoryBase(DemoBackendDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
            Queryable = DbSet;
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity, bool autoSave = false)
        {
            await Context.AddAsync(entity);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            Context.RemoveRange(entities);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return Queryable;
        }

        public virtual async Task DeleteAsync(TEntity entity, bool autoSave = false)
        {
            DbSet.Remove(entity);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }
        }

        public virtual async Task DeleteAsync(TKey id, bool autoSave = false)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity != null)
            {
                await DeleteAsync(entity, autoSave);
            }
        }
        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            return AttachIncludes(includes).FirstOrDefaultAsync(predicate);
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false)
        {
            if (includeDetails)
            {
                var navigations = Context.Model.FindEntityType(typeof(TEntity)).GetNavigations();
                if (navigations != null && navigations.Any())
                {
                    foreach (var property in navigations)
                    {
                        Queryable = Queryable.Include(property.Name);
                    }
                }
            }
            return Queryable.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            var entity = await AttachIncludes(includes).AsNoTracking().FirstOrDefaultAsync(predicate);

            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        private async Task IncludeDetails(TEntity model)
        {
            var navigations = Context.Model.FindEntityType(model.GetType()).GetNavigations();
            if (navigations != null && navigations.Any())
            {
                foreach (var property in navigations)
                {
                    var propertyType = property.PropertyInfo.PropertyType;
                    if (propertyType.IsGenericType)
                    {
                        await Context.Entry(model).Collection(property.Name).LoadAsync();
                    }
                    else
                    {
                        await Context.Entry(model).Reference(property.Name).LoadAsync();
                    }
                }
            }
        }

        public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = false)
        {
            var model = await DbSet.FindAsync(id);
            if (model == null)
            {
                throw new Exception($"There is no such an entity. Entity type: {typeof(TEntity).Name}, id: {id}");
            }
            if (includeDetails)
            {
                await IncludeDetails(model);
            }
            return model;
        }

        public virtual Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, params string[] includes)
        {
            if (predicate == null)
            {
                return AttachIncludes(includes).ToListAsync();
            }
            else
            {
                return AttachIncludes(includes).Where(predicate).ToListAsync();
            }
        }

        public virtual Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, bool includeDetails = false)
        {
            if (includeDetails)
            {
                var navigations = Context.Model.FindEntityType(typeof(TEntity)).GetNavigations();
                if (navigations != null && navigations.Any())
                {
                    foreach (var property in navigations)
                    {
                        Queryable = Queryable.Include(property.Name);
                    }
                }
            }

            return predicate == null ? Queryable.ToListAsync() : Queryable.Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false)
        {
            DbSet.Update(entity);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }
            return entity;
        }



        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? DbSet.CountAsync() : DbSet.Where(predicate).CountAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AnyAsync(predicate);
        }

        private IQueryable<TEntity> AttachIncludes(params string[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return Queryable;
            }


            foreach (var include in includes)
            {
                Queryable = Queryable.Include(include);
            }

            return Queryable;
        }
    }

    #endregion

    #endregion

}
