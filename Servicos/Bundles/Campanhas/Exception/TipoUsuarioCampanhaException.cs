using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicos.Bundles.Campanhas
{
    public class TipoUsuarioCampanhaException : Exception
    {
        public TipoUsuarioCampanhaException() : base("Não é permitido criar campanhas para o tipo de usuário colaborador.")
        {
        }
    }
}