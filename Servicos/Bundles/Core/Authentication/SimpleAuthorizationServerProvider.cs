using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Servicos.Bundles.Core.Repository;
using Servicos.Bundles.Pessoas.Entity;
using Servicos.Bundles.Pessoas.Resource;
using Servicos.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Servicos.Bundles.Core.Authentication
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Autorização de domínios
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            IRepository repository = new AbstractEditableEntityRepository(new ServicosContext());
            UsuarioService usuarioService = new UsuarioService(repository);
            var usuario = usuarioService.GetAll(context.UserName, context.Password).FirstOrDefault();
            if (usuario == null)
            {
                context.SetError("invalid_grant", new LoginSenhaIncorretaException().Message);
                return;
            }
            if (usuario.SenhaExpirada && !usuario.SuperAdmin)
            {
                context.SetError("invalid_grant", new SenhaExpiradaException().Message);
                return;
            }

            var permissao = usuario.SuperAdmin ? "SUPER_ADMIN" : "COMUM";
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("usuario", context.UserName));
            identity.AddClaim(new Claim("permissao", permissao));
            identity.AddClaim(new Claim("id", usuario.Id.ToString()));

            //IDictionary<string, string> data = new Dictionary<string, string>
            //{
            //    { "user", Newtonsoft.Json.JsonConvert.SerializeObject(usuario) }
            //};
            //AuthenticationProperties properties = new AuthenticationProperties(data);
            //AuthenticationTicket ticket = new AuthenticationTicket(identity, properties);

            context.Validated(identity);
        }
    }
}