using Servicos.Bundles.Core.Repository;
using Servicos.Bundles.Core.Resource;
using Servicos.Bundles.Pessoas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Servicos.Bundles.Pessoas.Resource
{
    public class TelefoneService : AbstractService<Telefone>
    {
        public TelefoneService(AbstractEntityRepository repository)
            : base(repository)
        {

        }

        public IEnumerable<Telefone> GetByParams(int pessoa, string tipo)
        {
            if (pessoa > 0)
                _parameters.Add(t => t.Pessoa.Id == pessoa);
            if (!string.IsNullOrWhiteSpace(tipo))
                _parameters.Add(t => t.Tipo == tipo);

            return base.GetAll();
        }

        public override void AfterUpdate(Telefone telefone)
        {
            _repository.GetEntry<Pessoa>(telefone.Pessoa).State = EntityState.Unchanged;
        }
    }
}