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
using Servicos.Bundles.Pessoas.Resource;

namespace Servicos.Bundles.Pessoas.Controller
{
    [Route("api/telefones")]
    public class TelefoneController : ApiController
    {
        private readonly TelefoneService _service;
        public TelefoneController(TelefoneService service)
        {
            _service = service;
        }

        [HttpGet]
        public HttpResponseMessage GetAll(int pessoa = 0, string tipo = "")
        {
            IEnumerable<Telefone> telefones = _service.GetByParams(pessoa, tipo);
            return Request.CreateResponse(HttpStatusCode.OK, telefones);
        }

        [HttpGet]
        [Route("api/telefones/{id}")]
        public HttpResponseMessage GetOne(int id)
        {
            Telefone telefone = _service.GetOne(id);
            return Request.CreateResponse(HttpStatusCode.OK, telefone);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Telefone telefone)
        {
            _service.Add(telefone);
            return Request.CreateResponse(HttpStatusCode.OK, telefone);
        }

        [HttpPut]
        [Route("api/telefones/{id}")]
        public HttpResponseMessage Put(int id, [FromBody]Telefone telefone)
        {
            _service.Update(telefone);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/telefones/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            _service.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}