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
        private readonly IRepository _repository;
        public DoacaoController(DoacaoService service, IRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpPost]
        public HttpResponseMessage Post(Doacao doacao)
        {
            List<string> mensagensErro;

            Animal animal = doacao.Animal;
            mensagensErro = ValidarAnimal(animal);

            if (mensagensErro.Count > 0)
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = false, mensagensErro });

            _repository.Add<Animal>(animal);
            _repository.Commit();


            Doacao novaDoacao = new Doacao(animal, doacao.Usuario);
            _service.Add(novaDoacao);

            return Request.CreateResponse(HttpStatusCode.OK, new {status = true, novaDoacao });
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

        private List<string> ValidarAnimal(Animal a)
        {
            List<string> mensagensErro = new List<string>();

            if (!a.Ativo)
                mensagensErro.Add("O cadastro do animal deve estar ativo para ser doado");

            if (string.IsNullOrEmpty(a.Nome))
                mensagensErro.Add("O nome do animal é obrigatório");

            if (string.IsNullOrEmpty(a.Descricao))
                mensagensErro.Add("A descrição do animal é obrigatória");
            
            if (string.IsNullOrEmpty(a.Especie))
                mensagensErro.Add("A espécie do animal é obrigatória");

            if (a.Peso <= 0)
                mensagensErro.Add("O peso do animal deve ser maior que zero");

            return mensagensErro;
        }
    }
}
