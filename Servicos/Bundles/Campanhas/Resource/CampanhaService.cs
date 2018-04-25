using Servicos.Bundles.Campanhas.Entity;
using Servicos.Bundles.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servicos.Bundles.Core.Repository;

namespace Servicos.Bundles.Campanhas.Resource
{
    public class CampanhaService : AbstractService<Campanha>
    {
        public CampanhaService(IRepository repository) : base(repository)
        {
        }

        public IEnumerable<Campanha> Get(int usuario = 0)
        {
            if (usuario != 0)
                _parameters.Add(c => c.Usuario.Id == usuario);
            return base.GetAll();
        }

        public override void BeforeCreate(Campanha campanha)
        {
            if (campanha.Usuario.Tipo != "ORGANIZACAO")
                throw new TipoUsuarioCampanhaException();
        }
    }
}