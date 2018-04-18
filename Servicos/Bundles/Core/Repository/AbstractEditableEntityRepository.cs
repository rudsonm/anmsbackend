using Servicos.Bundles.Core.Entity;
using Servicos.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Servicos.Bundles.Core.Repository
{
    public class AbstractEditableEntityRepository : AbstractEntityRepository
    {
        public AbstractEditableEntityRepository(ServicosContext dbContext)
            : base(dbContext)
        {

        }

        public void Update<T>(T entity) where T : AbstractEditableEntity
        {
            entity.DataModificacao = DateTime.Now;
            base.Update(entity);
        }
    }
}