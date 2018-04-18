using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicos.Bundles.Core
{
    public class SenhaExpiradaException : Exception
    {
        public SenhaExpiradaException()
            : base("Senha Expirada")
        {

        }
    }
}