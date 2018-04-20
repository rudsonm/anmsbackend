using Servicos.Bundles.Campanhas.Entity;
using Servicos.Bundles.Campanhas.Resource;
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
        public HttpResponseMessage Get()
        {
            var campanhas = _service.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, campanhas);
        }

        [HttpPost]
        public HttpResponseMessage Post(Campanha campanha)
        {
            _service.Add(campanha);
            return Request.CreateResponse(HttpStatusCode.OK, campanha);
        }
    }
}
