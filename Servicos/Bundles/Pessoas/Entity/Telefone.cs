using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicos.Bundles.Core.Entity;

namespace Servicos.Bundles.Pessoas.Entity
{
    public class Telefone : AbstractEntity
    {
        public int Numero { get; set; }
        public string Tipo { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
