using LeagueViewer.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LeagueViewer.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        internal LeagueViewerContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(LeagueViewerContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, 
                IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }
                foreach (var includeProperty in
                    includeProperties.Split
                    (new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
            catch (Exception e)
            {
                throw new PersistentStoreException("Ocurrió un error al acceder a la base de datos", e);
            }
        }

        public virtual TEntity GetByID(object id)
        {
            try
            {
                return dbSet.Find(id);
            }
            catch (Exception e)
            {
                throw new PersistentStoreException("Ocurrió un error al acceder a la base de datos", e);
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            try
            {
                if (context.Entry(entityToDelete).State ==
                EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }
            catch (Exception e)
            {
                throw new PersistentStoreException("Ocurrió un error al acceder a la base de datos", e);
            }
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = GetByID(id);
            Delete(entityToDelete);
        }

        public virtual TEntity GetByFilterWithOtherProperties(
            Expression<Func<TEntity, bool>> filter, 
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = dbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                foreach (var includeProperty in
                    includeProperties.Split
                    (new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                return query.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new PersistentStoreException("Ocurrió un error al acceder a la base de datos", e);
            }
            
        }

        public virtual void Insert(TEntity entity)
        {
            try
            {
                dbSet.Add(entity);
            }
            catch (Exception e)
            {
                throw new PersistentStoreException("Ocurrió un error al acceder a la base de datos", e);
            }
        }


        public virtual void Update(TEntity entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State =
                    EntityState.Modified;
            }
            catch (Exception e)
            {
                throw new PersistentStoreException("Ocurrió un error al acceder a la base de datos", e);
            }
        }

        public virtual void DeleteAll(List<TEntity> entities)
        {
            try
            {
                dbSet.RemoveRange(entities);
            }
            catch (Exception e)
            {
                throw new PersistentStoreException("Ocurrió un error al acceder a la base de datos", e);
            }
        }

        public virtual bool Exists(
            Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                TEntity tEntity =
                    GetByFilterWithOtherProperties(filter);
                return tEntity != null;
            }
            catch (Exception e)
            {
                throw new PersistentStoreException("Ocurrió un error al acceder a la base de datos", e);
            }
        }
    }
}
