namespace Servicos.Bundles.Core.Entity
{
    public class AbstractFoto : AbstractEntity
    {
        public byte[] Bytes { get; set; }
        public string MimeType { get; set; }

        public AbstractFoto(byte[] bytes, string mimeType)
        {
            this.Bytes = bytes;
            this.MimeType = mimeType;
        }
    }
}