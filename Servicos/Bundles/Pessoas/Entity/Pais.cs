using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Bundles.Pessoas.Entity
{
    public class Pais
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
