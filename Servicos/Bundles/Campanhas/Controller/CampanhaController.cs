using Servicos.Bundles.Campanhas.Entity;
using Servicos.Bundles.Campanhas.Resource;
using System;
using System.Collections.Generic;
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
                var mensagensErro = ValidarCampanha(campanha);
                if (mensagensErro.Count == 0)
                {
                    _service.Add(campanha);
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = true, campanha });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = false, mensagensErro });
                }
            } catch (Exception e) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut]
        [Route("api/campanhas/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] Campanha campanha)
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

        private List<string> ValidarCampanha(Campanha c)
        {
            List<string> retorno = new List<string>();

            if (string.IsNullOrEmpty(c.Descricao))
                retorno.Add("A descrição da campanha é obrigatória");

            if (c.DataInicio == null)
                retorno.Add("A data de início da campanha é obrigatória");

            if (string.IsNullOrEmpty(c.Titulo))
                retorno.Add("O título da campanha é obrigatório");

            if (c.Meta <= 0)
                retorno.Add("A meta da campanha deve ser maior que zero");

            return retorno;
        }
    }
}
