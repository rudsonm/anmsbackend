using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicos.Bundles.Core
{
    public class LoginSenhaIncorretaException : Exception
    {
        public LoginSenhaIncorretaException()
            : base("Login ou Senha está incorreto")
        {

        }
    }
}