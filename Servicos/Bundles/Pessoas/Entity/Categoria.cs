using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servicos.Bundles.Core.Entity;

namespace Servicos.Bundles.Pessoas.Entity
{
    public class Categoria : AbstractEntity
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
    }
}