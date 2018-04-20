using Servicos.Bundles.Campanhas.Entity;
using Servicos.Bundles.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servicos.Bundles.Core.Repository;

namespace Servicos.Bundles.Campanhas.Resource
{
    public class CampanhaService : AbstractService<Campanha>
    {
        public CampanhaService(IRepository repository) : base(repository)
        {
        }
    }
}