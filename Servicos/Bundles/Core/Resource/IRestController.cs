using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Servicos.Bundles.Core.Resource
{
    interface IRestController<T>
    {
        [HttpGet]
        HttpResponseMessage Get();
        [HttpGet]
        HttpResponseMessage GetOne(int id);
        [HttpPost]
        HttpResponseMessage Post(T entity);
        [HttpPut]
        HttpResponseMessage Put(int id, T entity);
        [HttpDelete]
        HttpResponseMessage Delete(int id);
    }
}
