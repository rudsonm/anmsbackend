using Servicos.Bundles.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicos.Bundles.Core.Resource
{
    public interface IService<T>
    {
        T Add(T entity);
        void Update(T entity);
        void Remove(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByParams(Dictionary<string, object> parameters);
        T GetOne(int id);
        void BeforeCreate(T entity);
        void AfterCreate(T entity);
        void BeforeUpdate(T entity);
        void AfterUpdate(T entity);

    }
}