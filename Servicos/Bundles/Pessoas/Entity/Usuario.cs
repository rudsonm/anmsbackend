using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicos.Bundles.Core.Entity;

namespace Servicos.Bundles.Pessoas.Entity
{
    public class Usuario : Pessoa
    {
        public Usuario() : base()
        {
            SuperAdmin = false;
            SenhaExpirada = false;
        }
        public string Senha { get; set; }
        public bool SenhaExpirada { get; set; }
        public bool SuperAdmin { get; set; }
    }
}
