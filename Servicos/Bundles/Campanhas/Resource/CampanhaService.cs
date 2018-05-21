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

        public IEnumerable<Campanha> Get(int usuario = 0, string status = "")
        {
            if (usuario != 0)
                _parameters.Add(c => c.Usuario.Id == usuario);
            if (!string.IsNullOrWhiteSpace(status))
                _parameters.Add(c => c.Status == status);
            return base.GetAll();
        }

        public override void BeforeCreate(Campanha campanha)
        {
            if (campanha.Usuario.Tipo != "ORGANIZACAO")
                throw new TipoUsuarioCampanhaException();
        }

        public override void Update(Campanha campanha)
        {
            this.BeforeUpdate(campanha);
            Campanha c = _repository.GetOne<Campanha>(campanha.Id);
            c.Status = campanha.Status;
            _repository.Commit();
        }

        public override void BeforeUpdate(Campanha campanha)
        {
            string status = base.GetOne(campanha.Id).Status;
            switch(campanha.Status)
            {
                case "EM_ANDAMENTO":
                    if (status.Equals("CANCELADO") || status.Equals("FINALIZADO"))
                        throw new Exception("Não é possível continuar uma campanha encerrada.");
                    break;
                case "PAUSADO":
                    if (status.Equals("CANCELADO") || status.Equals("FINALIZADO"))
                        throw new Exception("Não é possível pausar uma campanha encerrada.");
                    if (status.Equals("PAUSADO"))
                        throw new Exception("A campanha já está pausada.");
                    break;
                // REVER 
                case "FINALIZADO":
                    if (!status.Equals("EM_ANDAMENTO"))
                        throw new Exception("Não é possível finalizar uma campanha que não está em andamento.");
                    break;
                case "CANCELADO":
                    if (!status.Equals("EM_ANDAMENTO"))
                        throw new Exception("Não é possível cancelar uma campanha que não está em andamento.");
                    break;                
            }
        }

        public override void OnDelete(int id)
        {
            Campanha campanha = base.GetOne(id);
            if (campanha.DataInicio > new DateTime())
                throw new CampanhaPrazoExclusao();
        }
    }
}