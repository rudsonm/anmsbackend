using Servicos.Bundles.Pessoas.Entity;
using Servicos.Bundles.Pessoas.Resource;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicos.Bundles.Pessoas.Controller
{
    [Route("api/usuarios")]
    public class UsuarioController : ApiController
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public HttpResponseMessage GetAll(string username = "", string password = "", string cpf = "", string email = "")
        {
            IEnumerable<Usuario> usuarios = _service.GetAll(username, password, cpf, email);
            return Request.CreateResponse(HttpStatusCode.OK, usuarios);
        }

        [HttpGet]
        [Route("api/usuarios/{id}")]
        public HttpResponseMessage GetOne(int id)
        {
            Usuario usuario = _service.GetOne(id);
            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Post(Usuario usuario)
        {
            _service.Add(usuario);
            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/usuarios/recuperarsenha/{id}")]
        public HttpResponseMessage RecuperarSenha(int id)
        {
            try
            {
                _service.EnviarEmailRecuperacaoSenha(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }

        }
    }
}
