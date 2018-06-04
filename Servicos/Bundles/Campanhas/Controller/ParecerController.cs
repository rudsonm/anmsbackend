using Servicos.Bundles.Campanhas.Entity;
using Servicos.Bundles.Core.Repository;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicos.Bundles.Campanhas.Controller
{
    [Route("api/pareceres")]
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

        [Route("api/campanhas/{campanha}/pareceres")]
        [HttpGet]
        public HttpResponseMessage GetByDoacao(int campanha)
        {
            var pareceres = _repository.GetAll<Parecer>().Where(p => p.Campanha.Id == campanha);
            return Request.CreateResponse(HttpStatusCode.OK, pareceres);
        }

        [HttpPost]
        public HttpResponseMessage Post(Parecer parecer)
        {
            if (string.IsNullOrWhiteSpace(parecer.Descricao))
                return Request.CreateResponse(HttpStatusCode.BadRequest, new string[] { "Descreva o que foi realizado com as colaborações monetárias." });

            _repository.Add(parecer);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, parecer);
        }
    }
}