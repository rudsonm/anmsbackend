using Servicos.Bundles.Core.Repository;
using Servicos.Bundles.Pessoas.Entity;
using Servicos.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Servicos.Bundles.Pessoas.Controller
{
    [Route("api/cidades")]
    public class CidadeController : ApiController
    {
        private readonly AbstractRepository _repository;
        public CidadeController(AbstractRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            IEnumerable<Cidade> cidades = _repository.GetAll<Cidade>();
            return Request.CreateResponse(HttpStatusCode.OK, cidades);
        }

        [HttpPost]
        public HttpResponseMessage Post(Cidade cidade)
        {
            _repository.Add<Cidade>(cidade);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, cidade);
        }

        [HttpPut]
        [Route("api/cidades/{id}")]
        public HttpResponseMessage Put(int id, Cidade cidade)
        {
            _repository.Update<Cidade>(cidade);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        [HttpDelete]
        [Route("api/cidades/{id}")]
        public HttpResponseMessage Delete(int id, Cidade cidade)
        {
            _repository.Remove<Cidade>(cidade);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}