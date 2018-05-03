using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicos.Bundles.Campanhas
{
    public class CampanhaPrazoExclusao : Exception
    {
        public CampanhaPrazoExclusao() : base("Não é possível remover uma campanha após seu período de início.")
        {

        }
    }
}