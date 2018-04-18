using System.Data.Entity;
using Servicos.Bundles.Pessoas.Entity;
using Servicos.Bundles.Animais.Entity;

namespace Servicos.Context
{
    public class ServicosContext : DbContext
    {
        /*public ServicosContext()
            : base("db_connection_string")
        {

        }*/
        public ServicosContext()
            : base()
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ServicosContext>());
        }

        /* Pessoa Bundle */
        public IDbSet<Categoria> Categorias { get; set; }
        public IDbSet<Cidade> Cidades { get; set; }
        public IDbSet<Endereco> Enderecos { get; set; }
        public IDbSet<Estado> Estados { get; set; }
        public IDbSet<Fornecedor> Fornecedores { get; set; }
        public IDbSet<Foto> Fotos { get; set; }
        public IDbSet<Pais> Paises { get; set; }
        public IDbSet<Pessoa> Pessoas { get; set; }
        public IDbSet<Telefone> Telefones { get; set; }
        public IDbSet<Usuario> Usuarios { get; set; }

        /* Animal Bundle */
        public IDbSet<Animal> Animais { get; set; }
        public IDbSet<Doacao> Doacoes { get; set; }
        public IDbSet<Comentario> Comentarios { get; set; }
        public IDbSet<SolicitacaoAdocao> SolicitacoesAdocao { get; set; }
    }
}