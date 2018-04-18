using Servicos.Bundles.Core.Entity;
using Servicos.Bundles.Pessoas.Entity;
using System;

namespace Servicos.Bundles.Animais.Entity
{
    public class Comentario : AbstractEditableEntity
    {
        public Doacao Doacao { get; set; }
        public Usuario Remetente { get; set; }
        public String Mensagem { get; set; }

        public override string[] GetIncludeProperties()
        {
            return new string[] { "Remetente" };
        }
    }
}