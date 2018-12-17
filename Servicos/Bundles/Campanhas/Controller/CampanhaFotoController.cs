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

namespace Servicos.Bundles.Campanhas.Controller
{
    [RoutePrefix("api/campanhas/fotos")]
    public class CampanhaFotoController : ApiController
    {
        private readonly AbstractEntityRepository _repository;
        
        public CampanhaFotoController()
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
            int campanha = Int32.Parse(HttpContext.Current.Request.Params["EntidadeId"].ToString());
            foreach(HttpPostedFile file in files)
            {
                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);
                CampanhaFoto campanhaFoto = new CampanhaFoto(
                    campanha,
                    ms.ToArray(),
                    file.ContentType
                );
                _repository.Add(campanhaFoto);
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
