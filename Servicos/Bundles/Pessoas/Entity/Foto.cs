using Servicos.Bundles.Animais.Entity;
using Servicos.Bundles.Core.Entity;

namespace Servicos.Bundles.Pessoas.Entity
{
    public class Foto : AbstractEntity
    {
        public Foto() { }
        public Foto(Animal animal, byte[] bytes, string tipo)
        {
            this.Animal = animal;
            this.Bytes = bytes;
            this.Tipo = tipo;
        }
        public string Tipo { get; set; }
        public byte[] Bytes { get; set; }
        public virtual Animal Animal { get; set; }
    }
}