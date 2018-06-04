using Servicos.Bundles.Animais.Entity;
using Servicos.Bundles.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servicos.Bundles.Core.Repository;
using Servicos.Bundles.Pessoas.Entity;
using System.Web.UI;

namespace Servicos.Bundles.Animais.Resource
{
    public class DoacaoService : AbstractService<Doacao>
    {
        public DoacaoService(IRepository repository) : base(repository)
        {
        }

        public IEnumerable<Doacao> Get(string status, int animal, string animal_nome)
        {
            if (!string.IsNullOrWhiteSpace(status))
                base._parameters.Add(d => d.Status.Equals(status));
            if (animal != 0)
                base._parameters.Add(d => d.Animal.Id == animal);
            else if (!string.IsNullOrWhiteSpace(animal_nome))
                base._parameters.Add(d => d.Animal.Nome.Contains(animal_nome) || d.Animal.Especie.Contains(animal_nome));
            return base.GetAll();
        }

        public override void AfterUpdate(Doacao doacao)
        {
            var candidatos = _repository
                            .GetAll<SolicitacaoAdocao>()
                            .Where(s => s.Doacao.Id == doacao.Id && (s.Status.Equals("PENDENTE") || s.Status.Equals("SELECIONADO")))
                            .Select(s => new { s.Usuario.Email, s.Status });
            var emails = candidatos
                            .Where(p => !p.Status.Equals("SELECIONADO"))
                            .Select(c => c.Email)
                            .ToList();
            switch (doacao.Status)
            {
                case "CANCELADO":
                    EnviarNotificacaoCancelamento(doacao, emails);
                break;
                case "FINALIZADO":
                    var emailCandidatoAprovado = candidatos.First(c => c.Status.Equals("SELECIONADO")).Email;
                    EnviarNotificacaoNaoElegidos(doacao, emails);
                    EnviarNotificacaoAprovado(doacao, emailCandidatoAprovado);
                break;
            }
        }

        private void EnviarNotificacaoCancelamento(Doacao doacao, List<string> emails)
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

        private void EnviarNotificacaoNaoElegidos(Doacao doacao, List<string> emails)
        {
            string mensagem = string.Concat(
                "Você não foi escolhido para adotar o(a) ",
                doacao.Animal.Especie,
                " ",
                doacao.Animal.Nome
            );
            EnviadorEmail.EnviarMultiplos(emails.ToList(), "Doação Finalizada.", mensagem);
        }

        private void EnviarNotificacaoAprovado(Doacao doacao, string email)
        {
            string mensagem = string.Concat(
                "Parabéns, sua solicitação de adoção para o(a) ",
                doacao.Animal.Especie,
                " ",
                doacao.Animal.Nome,
                " acaba de ser aprovada!"
            );
            EnviadorEmail.Enviar(email, "Solicitação de Adoção Aprovada.", mensagem);
        }
    }
}