using Servicos.Bundles.Core.Entity;
using Servicos.Bundles.Pessoas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicos.Bundles.Campanhas.Entity
{
    public class Colaboracao : AbstractEditableEntity
    {
        public virtual Campanha Campanha { get; set; }
        public float Quantidade { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}