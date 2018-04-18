using Servicos.Bundles.Core.Repository;
using Servicos.Bundles.Pessoas.Entity;
using Servicos.Context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Servicos.Bundles.Pessoas.Controller
{
    [RoutePrefix("api/estados")]
    [Route]
    public class EstadoController : ApiController
    {
        private readonly AbstractRepository _repository;
        public EstadoController(AbstractRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            IEnumerable<Estado> estados = _repository.GetAll<Estado>();
            return Request.CreateResponse(HttpStatusCode.OK, estados);
        }

        [HttpPost]
        public HttpResponseMessage Post(Estado estado)
        {
            _repository.Add<Estado>(estado);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, estado);
        }

        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage Put(int id, Estado estado)
        {
            _repository.Update<Estado>(estado);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id, Estado estado)
        {
            _repository.Remove<Estado>(estado);
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("importar")]
        public async Task<HttpResponseMessage> Importar()
        {
            string apiUrl = ConfigurationManager.AppSettings["API_ESTADO"];            
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request);
            var content = response.Content.ReadAsStringAsync().Result;
            
            
            string[] splited = content.Split(new[] { "\\\"name\\\":" }, StringSplitOptions.None);
            foreach(string text in splited)
            {
                string nomeEstado = text.Split('\\')[0];
                nomeEstado = nomeEstado.Substring(1, nomeEstado.Length - 1);
                Estado estado = new Estado(nomeEstado);
                _repository.Add(estado);
            }
            //_repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, content);
        }
    }
}