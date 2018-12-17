using Servicos.Bundles.Campanhas.Entity;
using Servicos.Bundles.Core.Repository;
using Servicos.Context;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace Servicos.Bundles.Animiais.Controller
{
    [RoutePrefix("api/animais/fotos")]
    public class AnimalFotoController : ApiController
    {
        private readonly AbstractEntityRepository _repository;
        
        public AnimalFotoController()
        {            
            _repository = new AbstractEntityRepository(new ServicosContext());
        }
        
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetOne(int id)
        {
            CampanhaFoto foto = _repository.GetOne<CampanhaFoto>(id);
            if (!foto.Ativo || foto == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new ByteArrayContent(foto.Bytes);
            response.Content.LoadIntoBufferAsync(foto.Bytes.Length).Wait();
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(foto.MimeType);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage UploadImage()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            int animal = Int32.Parse(HttpContext.Current.Request.Params["EntidadeId"].ToString());
            foreach(HttpPostedFile file in files)
            {
                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);
                CampanhaFoto animalFoto = new CampanhaFoto(
                    animal,
                    ms.ToArray(),
                    file.ContentType
                );
                _repository.Add(animalFoto);
            }
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            _repository.Remove<CampanhaFoto>(id);
            _repository.Commit();
        }
    }
}
