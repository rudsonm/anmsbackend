using Servicos.Bundles.Core.Entity;
using Servicos.Context;
using System.ComponentModel.DataAnnotations;

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
