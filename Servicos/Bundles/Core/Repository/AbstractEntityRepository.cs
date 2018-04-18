using Servicos.Bundles.Core.Entity;
using Servicos.Context;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace Servicos.Bundles.Core.Repository
{
    public class AbstractEntityRepository : IRepository
    {
        protected ServicosContext _dbContext;        
        public AbstractEntityRepository(ServicosContext dbContext)
        {            
            _dbContext = dbContext;
        }

        public T Add<T>(T entity) where T : AbstractEntity
        {
            _dbContext.Entry<T>(entity).State = EntityState.Unchanged;
            _dbContext.Set<T>().Add(entity);
            return entity;
        }

        public virtual void Update<T>(T entity) where T : AbstractEntity
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Remove<T>(int id) where T : AbstractEntity
        {
            T entity = _dbContext.Set<T>().FirstOrDefault(e => e.Id == id);
            entity.Finalizar();
            Update(entity);
        }

        public IQueryable<T> GetAll<T>() where T : AbstractEntity
        {
            this._dbContext.Configuration.LazyLoadingEnabled = false;
            IQueryable<T> query = _dbContext.Set<T>().Where(t => t.Ativo);

            Type type = typeof(T);
            var instance = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("GetIncludeProperties");
            string[] properties = (string[]) method.Invoke(instance, null);
            foreach (string property in properties)
                query = query.Include(property);

            return query;
        }

        public T GetOne<T>(int id) where T : AbstractEntity
        {
            this._dbContext.Configuration.LazyLoadingEnabled = true;
            return _dbContext.Set<T>().FirstOrDefault(e => e.Id == id);            
        }
        
        public DbEntityEntry GetEntry<T>(T entity) where T : AbstractEntity
        {
            return _dbContext.Entry<T>(entity);
        }

        public void Commit()
        {            
            _dbContext.SaveChanges();
        }
    }
}