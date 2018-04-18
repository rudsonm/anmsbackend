using Servicos.Bundles.Core.Repository;

namespace Servicos.Bundles.Pessoas.Resource
{
    public class EstadoImporter
    {
        public readonly AbstractRepository _repository;
        public EstadoImporter(AbstractRepository repository)
        {
            _repository = repository;
        }
    }
}