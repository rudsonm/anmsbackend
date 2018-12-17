using Servicos.Bundles.Core.Entity;

namespace Servicos.Bundles.Campanhas.Entity
{
    public class ParecerFoto : AbstractFoto
    {
        public int Parecer { get; set; }

        public ParecerFoto(int parecer, byte[] bytes, string mimeType) : base(bytes, mimeType)
        {
            this.Parecer = parecer;
        }
    }
}