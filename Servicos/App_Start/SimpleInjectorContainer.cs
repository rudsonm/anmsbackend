using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servicos.Context;
using Servicos.Bundles.Core.Repository;
using Servicos.Bundles.Core.Entity;
using Servicos.Bundles.Core.Resource;
using System.Web.Http;
using Servicos.Bundles.Pessoas.Resource;

namespace Servicos.App_Start
{
    public static class SimpleInjectorContainer
    {
        public static Container RegisterServices()
        {
            Container container = new Container();
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Register<ServicosContext, ServicosContext>();
            container.Register<IRepository, AbstractEditableEntityRepository>();
            container.Register<AbstractRepository, AbstractRepository>();
            container.Register<AbstractEntityRepository, AbstractEntityRepository>();
            container.Register<PessoaService, PessoaService>();
            container.Register<TelefoneService, TelefoneService>();
            container.Register<UsuarioService, UsuarioService>();

            container.Verify();

            return container;
        }
    }
}