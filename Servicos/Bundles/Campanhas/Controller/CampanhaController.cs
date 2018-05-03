using Servicos.Bundles.Campanhas.Entity;
using Servicos.Bundles.Campanhas.Resource;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicos.Bundles.Campanhas.Controller
{
    [Route("api/campanhas")]
    public class CampanhaController : ApiController
    {
        private readonly CampanhaService _service;
        public CampanhaController(CampanhaService service)
        {
            _service = service;
        }

        [HttpGet]
        public HttpResponseMessage Get(int usuario = 0, string status = "")
        {
            var campanhas = _service.Get(usuario, status);
            return Request.CreateResponse(HttpStatusCode.OK, campanhas);
        }

        [HttpGet]
        [Route("api/campanhas/{id}")]
        public HttpResponseMessage GetOne(int id)
        {
            Campanha campanha = _service.GetOne(id);
            return Request.CreateResponse(HttpStatusCode.OK, campanha);
        }

        [HttpPost]
        public HttpResponseMessage Post(Campanha campanha)
        {
            try {
                _service.Add(campanha);
                return Request.CreateResponse(HttpStatusCode.OK, campanha);
            } catch (Exception e) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put(Campanha campanha)
        {
            _service.Update(campanha);
            return Request.CreateResponse(HttpStatusCode.OK, campanha);
        }

        [HttpDelete]
        [Route("api/campanhas/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try {
                _service.Remove(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            } catch (Exception e) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }            
        }
    }
}
