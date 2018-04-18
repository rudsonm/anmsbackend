using System;
using System.Collections.Generic;
using System.Linq;
using Servicos.Bundles.Core.Resource;
using System.Web;
using Servicos.Bundles.Core.Repository;
using Servicos.Context;
using System.Data.Entity;
using System.Linq.Expressions;
using Servicos.Bundles.Core.Entity;

namespace Servicos.Bundles.Core.Resource
{
    public abstract class AbstractService<T> where T : AbstractEntity
    {
        protected readonly IRepository _repository;
        protected List<Expression<Func<T, Boolean>>> _parameters;

        public AbstractService(IRepository repository)
        {            
            _repository = repository;
            _parameters = new List<Expression<Func<T, Boolean>>>();
        }

        public T Add(T entity)
        {
            BeforeCreate(entity);
            _repository.Add(entity);
            AfterCreate(entity);
            _repository.Commit();
            return entity;
        }

        public void Update(T entity)
        {
            BeforeUpdate(entity);
            _repository.Update(entity);
            AfterUpdate(entity);
            _repository.Commit();
        }

        public void Remove(int id)
        {
            OnDelete(id);
            _repository.Remove<T>(id);
            _repository.Commit();
        }

        public virtual IEnumerable<T> GetAll()
        {
            IQueryable<T> entities = _repository.GetAll<T>();

            foreach(Expression<Func<T, Boolean>> param in _parameters)
                entities = entities.Where(param);

            return entities.ToList();
        }

        public virtual T GetOne(int id)
        {
            return _repository.GetOne<T>(id);
        }

        public virtual void BeforeCreate(T entity)
        {

        }
        public virtual void AfterCreate(T entity)
        {

        }
        public virtual void BeforeUpdate(T entity)
        {

        }
        public virtual void AfterUpdate(T entity)
        {

        }

        public virtual void OnDelete(int id)
        {

        }
    }
}