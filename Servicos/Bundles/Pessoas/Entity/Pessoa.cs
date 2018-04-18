using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicos.Bundles.Core.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Servicos.Bundles.Pessoas.Entity
{
    public class Pessoa : AbstractEditableEntity
    {
        public string Nome { get; set; }
        public string Apelido { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
