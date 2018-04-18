using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicos.Bundles.Core.Entity;

namespace Servicos.Bundles.Pessoas.Entity
{
    public class Endereco : AbstractEditableEntity
    {
        public int Cep { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual Cidade Cidade { get; set; }
    }
}
