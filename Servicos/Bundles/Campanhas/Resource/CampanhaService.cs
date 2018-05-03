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

        public IEnumerable<Campanha> Get(int usuario = 0, string status = "")
        {
            if (usuario != 0)
                _parameters.Add(c => c.Usuario.Id == usuario);
            if (!string.IsNullOrWhiteSpace(status))
                _parameters.Add(c => c.Status == status);
            return base.GetAll();
        }

        public override void BeforeCreate(Campanha campanha)
        {
            if (campanha.Usuario.Tipo != "ORGANIZACAO")
                throw new TipoUsuarioCampanhaException();
        }

        public override void OnDelete(int id)
        {
            Campanha campanha = this.GetOne(id);
            if (campanha.DataInicio > new DateTime())
                throw new CampanhaPrazoExclusao();
        }
    }
}