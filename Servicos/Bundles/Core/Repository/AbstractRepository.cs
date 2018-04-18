using Servicos.Bundles.Core.Entity;
using Servicos.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Servicos.Bundles.Core.Repository
{
    public class AbstractRepository
    {
        private ServicosContext _dbContext;       
        public AbstractRepository(ServicosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IEnumerable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public virtual T Add<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Unchanged;
            _dbContext.Set<T>().Add(entity);
            return entity;
        }

        public virtual void Remove<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void Update<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}