using Servicos.Bundles.Animais.Entity;
using Servicos.Bundles.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servicos.Bundles.Core.Repository;
using Servicos.Bundles.Pessoas.Entity;

namespace Servicos.Bundles.Animais.Resource
{
    public class DoacaoService : AbstractService<Doacao>
    {
        public DoacaoService(IRepository repository) : base(repository)
        {
        }

        public IEnumerable<Doacao> Get(string status)
        {
            if (!string.IsNullOrWhiteSpace(status))
                base._parameters.Add(d => d.Status.Equals(status));

            return base.GetAll();
        }

        public override void AfterUpdate(Doacao doacao)
        {
            switch(doacao.Status)
            {
                case "CANCELADO":
                    var emails = _repository
                            .GetAll<SolicitacaoAdocao>()
                            .Where(s => s.Doacao.Id == doacao.Id)
                            .Select(s => s.Usuario.Email);
                    EnviarNotificacaoCancelamento(doacao, emails);
                break;
                case "FINALIZADO":
                break;
            }
        }

        private void EnviarNotificacaoCancelamento(Doacao doacao, IEnumerable<string> emails)
        {
            string mensagem = string.Concat(
                        "A doação do(a) ",
                        doacao.Animal.Especie,
                        " ",
                        doacao.Animal.Nome,
                        " acaba de ser cancelada por seu responsável."
                    );
            EnviadorEmail.EnviarMultiplos(emails, "Doação Cancelada.", mensagem);
        }

        private void EnviarNotificacaoNaoElegidos(Doacao doacao, IEnumerable<string> emails)
        {
            string mensagem = string.Concat(
                        "Você não foi escolhido para adotar o(a)",
                        doacao.Animal.Especie,
                        " ",
                        doacao.Animal.Nome
                    );
            EnviadorEmail.EnviarMultiplos(emails, "Doação Finalizada.", mensagem);
        }
    }
}