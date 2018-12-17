using Servicos.Bundles.Core.Entity;

namespace Servicos.Bundles.Campanhas.Entity
{
    public class CampanhaFoto : AbstractFoto
    {
        public int Campanha { get; set; }

        public CampanhaFoto(int campanha, byte[] bytes, string mimeType) : base(bytes, mimeType)
        {
            this.Campanha = campanha;
        }
    }
}