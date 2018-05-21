using Servicos.Bundles.Campanhas.Entity;
using Servicos.Bundles.Core.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicos.Bundles.Campanhas.Controller
{
    [Route("api/colaboracoes")]
    public class ColaboracaoController : ApiController
    {
        private readonly AbstractEditableEntityRepository _repository;
        public ColaboracaoController(AbstractEditableEntityRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public HttpResponseMessage Get(int usuario = 0, int campanha = 0)
        {            
            IQueryable<Colaboracao> colaboracoes = _repository.GetAll<Colaboracao>();
            if (usuario != 0)
                colaboracoes = colaboracoes.Where(c => c.Usuario.Id == usuario);
            if (campanha != 0)
                colaboracoes = colaboracoes.Where(c => c.Campanha.Id == campanha);
            return Request.CreateResponse(HttpStatusCode.OK, colaboracoes.ToList());
        }

        [HttpPost]
        public HttpResponseMessage Post(Colaboracao colaboracao)
        {
            _repository.Add<Colaboracao>(colaboracao);
            _repository.Commit();
            Campanha campanha = _repository.GetOne<Campanha>(colaboracao.Campanha.Id);
            campanha.Contribuicao += colaboracao.Quantidade;
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, colaboracao);
        }
    }
}
