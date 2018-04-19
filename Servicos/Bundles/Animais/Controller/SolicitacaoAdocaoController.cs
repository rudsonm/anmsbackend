using Servicos.Bundles.Animais.Entity;
using Servicos.Bundles.Animais.Resource;
using Servicos.Bundles.Core.Repository;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicos.Bundles.Animais.Controller
{
    [Route("api/solicitacoes-adocao")]
    public class SolicitacaoAdocaoController : ApiController
    {
        private readonly SolicitacaoAdocaoService _service;
        private readonly AbstractEditableEntityRepository _repository;
        public SolicitacaoAdocaoController(SolicitacaoAdocaoService service, AbstractEditableEntityRepository repository)
        {
            this._service = service;
            this._repository = repository;
        }

        [HttpGet]
        public HttpResponseMessage GetAll(int doacao = 0, int usuario = 0, string nome = "", string status = "")
        {
            var solicitacoes = _service.GetAll(doacao, usuario, nome, status);
            return Request.CreateResponse(HttpStatusCode.OK, solicitacoes);
        }

        [HttpGet]
        [Route("api/doacoes/{id}/solicitacoes-adocao")]
        public HttpResponseMessage GetByDoacao(int id)
        {
            var solicitacoes = _service.GetAll(id);
            return Request.CreateResponse(HttpStatusCode.OK, solicitacoes);
        }

        [HttpPost]
        public HttpResponseMessage Post(SolicitacaoAdocao solicitacaoAdocao)
        {
            _service.Add(solicitacaoAdocao);
            return Request.CreateResponse(HttpStatusCode.OK, solicitacaoAdocao);
        }

        [HttpPut]
        [Route("api/solicitacoes-adocao/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] SolicitacaoAdocao solicitacaoAdocao)
        {
            _service.Update(solicitacaoAdocao);
            return Request.CreateResponse(HttpStatusCode.OK, solicitacaoAdocao);
        }
    }
}
