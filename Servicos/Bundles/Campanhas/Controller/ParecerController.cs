using Servicos.Bundles.Campanhas.Entity;
using Servicos.Bundles.Core.Repository;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicos.Bundles.Campanhas.Controller
{
    [RoutePrefix("api/pareceres")]
    public class ParecerController : ApiController
    {
        private IRepository _repository;

        public ParecerController(IRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var pareceres = _repository.GetAll<Parecer>();
            return Request.CreateResponse(HttpStatusCode.OK, pareceres);
        }

        [HttpPost]
        public HttpResponseMessage Post(Parecer parecer)
        {
            _repository.Add(parecer);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, parecer);
        }
    }
}