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
        private readonly DoacaoService _doacaoService;
        public SolicitacaoAdocaoService(IRepository repository, DoacaoService doacaoService) : base(repository)
        {
            _doacaoService = doacaoService;
        }

        public IEnumerable<SolicitacaoAdocao> GetAll(int doacao = 0, int usuario = 0, string nome = "", string status = "")
        {
            if (doacao > 0)
                _parameters.Add(sa => sa.Doacao.Id == doacao);
            if (usuario > 0)
                _parameters.Add(sa => sa.Usuario.Id == usuario);
            if (!string.IsNullOrWhiteSpace(nome))
                _parameters.Add(sa => sa.Usuario.Nome.Contains(nome));
            if (!string.IsNullOrWhiteSpace(status))
                _parameters.Add(sa => sa.Status.Equals(status));
            return base.GetAll();
        }

        public override void AfterUpdate(SolicitacaoAdocao solicitacaoAdocao)
        {
            if(solicitacaoAdocao.Status == "SELECIONADO")
            {
                _repository.Commit();
                Doacao doacao = _doacaoService.GetOne(solicitacaoAdocao.Doacao.Id);
                doacao.Status = "FINALIZADO";
                _doacaoService.Update(doacao);
            }
        }
    }
}