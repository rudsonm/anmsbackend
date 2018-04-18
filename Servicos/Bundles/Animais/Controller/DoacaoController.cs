using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Servicos.Bundles.Core.Repository;
using Servicos.Bundles.Animais.Entity;
using Servicos.Bundles.Core.Resource;
using Servicos.Bundles.Animais.Resource;

namespace Servicos.Bundles.Animais.Controller
{
    [Route("api/doacoes")]
    public class DoacaoController : ApiController
    {
        private readonly DoacaoService _service;

        public DoacaoController(DoacaoService service)
        {
            _service = service;
        }

        [HttpPost]
        public HttpResponseMessage Post(Doacao doacao)
        {
            Animal animal = doacao.Animal;
            _service.Add(doacao);

            Doacao novaDoacao = new Doacao(animal, doacao.Usuario);
            _service.Add(doacao);

            return Request.CreateResponse(HttpStatusCode.OK, novaDoacao);
        }

        [HttpGet]
        public HttpResponseMessage Get(string status = "")
        {
            IEnumerable<Doacao> doacoes = _service.Get(status);
            return Request.CreateResponse(HttpStatusCode.OK, doacoes);
        }
        
        [HttpGet]
        [Route("api/doacoes/{id}")]
        public HttpResponseMessage GetOne(int id)
        {
            Doacao doacao = _service.GetOne(id);
            if (doacao == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nenhuma doação encontrada.");

            return Request.CreateResponse(HttpStatusCode.OK, doacao);
        }

        [HttpPut]
        [Route("api/doacoes/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] Doacao doacao)
        {
            _service.Update(doacao);
            return Request.CreateResponse(HttpStatusCode.OK, doacao);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            _service.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
