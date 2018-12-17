using Servicos.Bundles.Animais.Entity;
using Servicos.Bundles.Core.Repository;
using Servicos.Bundles.Core.Resource;
using Servicos.Bundles.Pessoas.Entity;
using Servicos.Context;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace Servicos.Bundles.Pessoas.Controller
{
    [RoutePrefix("api/pessoas/fotos")]
    public class ParecerFotoController : ApiController
    {
        private readonly AbstractEntityRepository _repository;
        
        public ParecerFotoController()
        {            
            _repository = new AbstractEntityRepository(new ServicosContext());
        }
        
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetOne(int id)
        {
            PessoaFoto foto = _repository.GetOne<PessoaFoto>(id);
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
            int pessoa = Int32.Parse(HttpContext.Current.Request.Params["EntidadeId"].ToString());
            foreach(HttpPostedFile file in files)
            {
                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);
                PessoaFoto pessoaFoto = new PessoaFoto(
                    pessoa,
                    ms.ToArray(),
                    file.ContentType
                );
                _repository.Add(pessoaFoto);
            }
            _repository.Commit();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            _repository.Remove<PessoaFoto>(id);
            _repository.Commit();
        }
    }
}
