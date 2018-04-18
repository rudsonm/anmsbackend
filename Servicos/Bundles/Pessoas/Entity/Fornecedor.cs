using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Bundles.Pessoas.Entity
{
    public class Fornecedor : Pessoa
    {
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string InscricaoEstadual { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
