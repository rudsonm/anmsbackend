using Servicos.Bundles.Pessoas.Entity;
using Servicos.Bundles.Pessoas.Resource;
using Servicos.Bundles.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Servicos.Context;
using Servicos.Bundles.Core.Resource;

namespace Servicos.Bundles.Pessoas.Controller
{
    [Route("api/pessoas")]
    public class PessoaController : ApiController
    {
        private readonly PessoaService _service;
        public PessoaController(PessoaService service)
        {
            _service = service;
        }

        [HttpGet]
        public HttpResponseMessage Get(string nome = "", string cpfCnpj = "", string email = "")
        {
            IEnumerable<Pessoa> pessoas = _service.GetAll(nome, cpfCnpj, email);
            return Request.CreateResponse(HttpStatusCode.OK, pessoas);
        }

        [HttpGet]
        [Route("api/pessoas/{id}")]
        public HttpResponseMessage GetOne(int id)
        {
            Pessoa pessoa = _service.GetOne(id);
            return Request.CreateResponse(HttpStatusCode.OK, pessoa);
        }
        
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Pessoa pessoa)
        {
            _service.Add(pessoa);
            return Request.CreateResponse(HttpStatusCode.OK, pessoa);
        }

        [HttpPut]
        [Route("api/pessoas/{id}")]
        public HttpResponseMessage Put(int id, [FromBody]Pessoa pessoa)
        {
            _service.Update(pessoa);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/pessoas/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            _service.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}