using Servicos.Bundles.Animais.Entity;
using Servicos.Bundles.Core.Repository;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicos.Bundles.Animais.Controller
{
    [Route("api/solicitacoes-adocao")]
    public class SolicitacaoAdocaoController : ApiController
    {
        private readonly AbstractEditableEntityRepository _repository;
        public SolicitacaoAdocaoController(AbstractEditableEntityRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            var solicitacoes = _repository.GetAll<SolicitacaoAdocao>();
            return Request.CreateResponse(HttpStatusCode.OK, solicitacoes);
        }

        [HttpGet]
        [Route("api/doacoes/{id}/solicitacoes-adocao")]
        public HttpResponseMessage GetByDoacao(int id)
        {
            var solicitacoes = _repository.GetAll<SolicitacaoAdocao>()
                                          .Where(s => s.Doacao.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK, solicitacoes);
        }

        [HttpPost]
        public HttpResponseMessage Post(SolicitacaoAdocao solicitacaoAdocao)
        {
            _repository.Add(solicitacaoAdocao);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, solicitacaoAdocao);
        }

        [HttpPut]
        public HttpResponseMessage Put(SolicitacaoAdocao solicitacaoAdocao)
        {
            _repository.Update(solicitacaoAdocao);
            if(solicitacaoAdocao.Status != SolicitacaoAdocaoStatus.PENDENTE)
            {
                Doacao doacao = _repository.GetOne<Doacao>(solicitacaoAdocao.Doacao.Id);
                //doacao.Status = DoacaoStatus.FINALIZADO;
                doacao.Status = "FINALIZADO";
                _repository.Update(doacao);
            }
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, solicitacaoAdocao);
        }
    }
}
