using Servicos.Bundles.Pessoas.Entity;
using Servicos.Bundles.Pessoas.Resource;
using Servicos.Context;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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
            var mensagensErro = ValidarUsuario(usuario);
            if (mensagensErro.Count == 0)
            {
                _service.Add(usuario);
                return Request.CreateResponse(HttpStatusCode.OK, new { status = true, usuario });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = false, mensagensErro });
            }
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

        private List<string> ValidarUsuario(Usuario u)
        {
            var retorno = new List<string>();

            if (string.IsNullOrEmpty(u.Email))
            {
                retorno.Add("o E-mail é obrigatório");
            }
            else
            {
                Regex regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

                if (!regexEmail.IsMatch(u.Email))
                    retorno.Add("E-mail inválido");
                else
                {
                    var usuarios = _service.GetAll();
                    foreach (var usuario in usuarios)
                    {
                        if (usuario.Email.Equals(u.Email))
                            retorno.Add("Já existe um usuário cadastrado com este E-mail");
                    }
                }
            }

            if (string.IsNullOrEmpty(u.Nome))
                retorno.Add("O nome é obrigatório");

            if (string.IsNullOrEmpty(u.Senha))
                retorno.Add("A senha é obrigatório");
            else if (u.Senha.Length < 6)
                retorno.Add("A senha deve possuir pelo menos seis dígitos");

            if (!CpfCnpjUtils.IsValid(u.CpfCnpj))
            {
                if (u.Tipo.Equals("COLABORADOR"))
                    retorno.Add("CPF inválido");
                else
                    retorno.Add("CNPJ inválido");
            }

            return retorno;
        }
    }
}
