using Servicos.Bundles.Animais.Entity;
using Servicos.Bundles.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicos.Bundles.Animais.Controller
{
    [Route("api/comentarios")]
    public class ComentarioController : ApiController
    {
        private readonly IRepository _repository;

        public ComentarioController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/doacoes/{doacao}/comentarios")]
        public HttpResponseMessage GetByDoacao(int doacao)
        {
            var comentarios = _repository.GetAll<Comentario>().Where(c => c.Doacao.Id == doacao);
            return Request.CreateResponse(HttpStatusCode.OK, comentarios);
        }

        [HttpGet]
        public HttpResponseMessage Get(int doacao)
        {
            var comentarios = _repository.GetAll<Comentario>().Where(c => c.Doacao.Id == doacao);
            return Request.CreateResponse(HttpStatusCode.OK, comentarios);
        }

        [HttpPost]
        public HttpResponseMessage Post(Comentario comentario)
        {
            _repository.Add<Comentario>(comentario);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, comentario);
        }
    }
}
