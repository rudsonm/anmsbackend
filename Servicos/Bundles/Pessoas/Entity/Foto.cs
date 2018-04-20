using Servicos.Bundles.Animais.Entity;
using Servicos.Bundles.Core.Entity;

namespace Servicos.Bundles.Pessoas.Entity
{
    public class Foto : AbstractEntity
    {
        public Foto() { }
        public Foto(string entidadeNome, int entidadeId, byte[] bytes, string tipo)
        {
            this.EntidadeNome = entidadeNome;
            this.EntidadeId = entidadeId;
            this.Bytes = bytes;
            this.Tipo = tipo;
        }
        public string EntidadeNome { get; set; }
        public int EntidadeId { get; set; }
        public string Tipo { get; set; }
        public byte[] Bytes { get; set; }
    }
}