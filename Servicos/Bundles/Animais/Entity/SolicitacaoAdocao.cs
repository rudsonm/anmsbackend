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
        [Description("ACEITO")]
        ACEITO,
        [Description("RECUSADO")]
        RECUSADO
    }

    public class SolicitacaoAdocao : AbstractEditableEntity
    {
        public SolicitacaoAdocao()
        {
            Status = SolicitacaoAdocaoStatus.PENDENTE;
        }
        public virtual Usuario Usuario { get; set; }
        public virtual Doacao Doacao { get; set; }
        public String Motivo { get; set; }
        public SolicitacaoAdocaoStatus Status { get; set; }

        public override string[] GetIncludeProperties()
        {
            return new string[] { "Usuario" };
        }
    }
}