using Servicos.Bundles.Core.Entity;

namespace Servicos.Bundles.Pessoas.Entity
{
    public class PessoaFoto : AbstractFoto
    {
        public int Pessoa { get; set; }

        public PessoaFoto(int pessoa, byte[] bytes, string mimeType) : base(bytes, mimeType)
        {
            this.Pessoa = pessoa;
        }
    }
}