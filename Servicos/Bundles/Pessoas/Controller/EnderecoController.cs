using System;
using System.Collections.Generic;
using Servicos.Bundles.Core.Repository;
using System.Linq;
using System.Web;
using System.Web.Http;
using Servicos.Bundles.Pessoas.Entity;
using System.Net.Http;
using System.Net;
using Servicos.Context;
using System.Data.Entity;

namespace Servicos.Bundles.Pessoas.Controller
{
    [Route("api/enderecos")]
    public class EnderecoController : ApiController
    {
        private readonly IRepository _repository;
        public EnderecoController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            IEnumerable<Endereco> enderecos = _repository.GetAll<Endereco>();
            return Request.CreateResponse(HttpStatusCode.OK, enderecos);
        }

        [HttpGet]
        [Route("api/enderecos/{id}")]
        public HttpResponseMessage GetOne(int id)
        {
            Endereco endereco = _repository.GetOne<Endereco>(id);
            return Request.CreateResponse(HttpStatusCode.OK, endereco);
        }
        
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Endereco endereco)
        {
            _repository.Add<Endereco>(endereco);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, endereco);
        }

        [HttpPut]
        [Route("api/enderecos/{id}")]
        public HttpResponseMessage Put(int id, [FromBody]Endereco endereco)
        {
            _repository.Update<Endereco>(endereco);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/enderecos/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            _repository.Remove<Endereco>(id);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}