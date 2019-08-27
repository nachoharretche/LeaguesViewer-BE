using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LeagueViewer.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        TEntity GetByID(object id);
        TEntity GetByFilterWithOtherProperties(
            Expression<Func<TEntity, bool>> filter, 
            string includeProperties = "");
        void Insert(TEntity entityToCreate);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        void DeleteAll(List<TEntity> entities);
        bool Exists(Expression<Func<TEntity, bool>> filter);
    }
}
