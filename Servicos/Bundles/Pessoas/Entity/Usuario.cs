namespace Servicos.Bundles.Pessoas.Entity
{
    public class Usuario : Pessoa
    {
        public Usuario() : base()
        {
            SuperAdmin = false;
            SenhaExpirada = false;
            Tipo = "COLABORADOR";
        }
        public string Senha { get; set; }
        public bool SenhaExpirada { get; set; }
        public bool SuperAdmin { get; set; }
        public string Tipo { get; set; }
    }
}