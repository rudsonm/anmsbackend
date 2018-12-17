using Servicos.Bundles.Core.Entity;

namespace Servicos.Bundles.Animais.Entity
{
    public class AnimalFoto : AbstractFoto
    {
        public int Animal { get; set; }

        public AnimalFoto(int animal, byte[] bytes, string mimeType) : base(bytes, mimeType)
        {
            this.Animal = animal;
        }
    }
}