using Servicos.Bundles.Core.Entity;
using Servicos.Context;
using System.Collections.Generic;
using System.Linq;

namespace Servicos.Bundles.Campanhas.Entity
{
    public class Parecer : AbstractEntity
    {
        public Campanha Campanha { get; set; }
        public string Descricao { get; set; }
        public virtual List<int> Fotos
        {
            get
            {
                return new ServicosContext().Fotos
                                            .Where(foto => foto.EntidadeId == this.Id && foto.EntidadeNome.Equals("Parecer"))
                                            .Select(foto => foto.Id)
                                            .ToList();
            }
        }
    }
}