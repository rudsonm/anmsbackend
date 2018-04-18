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
                    string mensagem = string.Concat(
                        "A doação do ",
                        doacao.Animal.Especie,
                        " ",
                        doacao.Animal.Nome,
                        " acaba de ser cancelada por seu responsável."
                    );
                    var emails = _repository
                                    .GetAll<SolicitacaoAdocao>()
                                    .Where(s => s.Doacao.Id == doacao.Id)
                                    .Select(s => s.Usuario.Email);
                    foreach (string email in emails)
                        EnviadorEmail.Enviar(email, "Doação Cancelada.", mensagem);
                break;
                case "FINALIZADO":
                break;
            }
        }
    }
}