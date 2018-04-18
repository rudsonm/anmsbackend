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
    [Route("api/paises")]
    public class PaisController : ApiController
    {
        private readonly AbstractRepository _repository;
        public PaisController(AbstractRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            IEnumerable<Pais> paises = _repository.GetAll<Pais>();
            return Request.CreateResponse(HttpStatusCode.OK, paises);
        }

        [HttpPost]
        public HttpResponseMessage Post(Pais pais)
        {
            _repository.Add<Pais>(pais);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, pais);
        }

        [HttpPut]
        [Route("api/paises/{id}")]
        public HttpResponseMessage Put(int id, Pais pais)
        {
            _repository.Update<Pais>(pais);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        [HttpDelete]
        [Route("api/paises/{id}")]
        public HttpResponseMessage Delete(int id, Pais pais)
        {
            _repository.Remove<Pais>(pais);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}