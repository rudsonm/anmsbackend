using Servicos.Bundles.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Bundles.Core.Repository
{
    public interface IRepository
    {
        T Add<T>(T entity) where T : AbstractEntity;
        void Update<T>(T entity) where T : AbstractEntity;
        void Remove<T>(int id) where T : AbstractEntity;
        IQueryable<T> GetAll<T>() where T : AbstractEntity;
        T GetOne<T>(int id) where T : AbstractEntity;
        DbEntityEntry GetEntry<T>(T entity) where T : AbstractEntity;
        void Commit();
    }
}
