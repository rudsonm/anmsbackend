using Servicos.Bundles.Core.Entity;
using Servicos.Bundles.Pessoas.Entity;
using System;
using System.ComponentModel;

namespace Servicos.Bundles.Animais.Entity
{
    public enum SolicitacaoAdocaoStatus
    {
        [Description("PENDENTE")]
        PENDENTE,
        [Description("SELECIONADO")]
        SELECIONADO,
        [Description("CANCELADO")]
        CANCELADO,
        [Description("RECUSADO")]
        RECUSADO
    }

    public class SolicitacaoAdocao : AbstractEditableEntity
    {
        public SolicitacaoAdocao()
        {
            //Status = SolicitacaoAdocaoStatus.PENDENTE;
            Status = "PENDENTE";
        }
        public virtual Usuario Usuario { get; set; }
        public virtual Doacao Doacao { get; set; }
        public string Motivo { get; set; }
        //public SolicitacaoAdocaoStatus Status { get; set; }
        public string Status { get; set; }

        public override string[] GetIncludeProperties()
        {
            return new string[] { "Usuario", "Doacao" };
        }
    }
}