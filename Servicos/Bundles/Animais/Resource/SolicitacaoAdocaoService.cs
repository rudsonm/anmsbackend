using Servicos.Bundles.Animais.Entity;
using Servicos.Bundles.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servicos.Bundles.Core.Repository;

namespace Servicos.Bundles.Animais.Resource
{
    public class SolicitacaoAdocaoService : AbstractService<SolicitacaoAdocao>
    {
        public SolicitacaoAdocaoService(IRepository repository) : base(repository)
        {
        }

        public IEnumerable<SolicitacaoAdocao> GetAll(int doacao, int usuario, string nome)
        {
            if (doacao > 0)
                _parameters.Add(sa => sa.Doacao.Id == doacao);
            if (usuario > 0)
                _parameters.Add(sa => sa.Usuario.Id == usuario);
            if (!string.IsNullOrWhiteSpace(nome))
                _parameters.Add(sa => sa.Usuario.Nome.Contains(nome));
            return base.GetAll();
        }
    }
}