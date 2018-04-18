using Servicos.Bundles.Core.Repository;
using Servicos.Bundles.Core.Resource;
using Servicos.Bundles.Pessoas.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servicos.Bundles.Pessoas.Resource
{
    public class UsuarioService : AbstractService<Usuario>
    {
        public UsuarioService(IRepository repository) : base(repository)
        {

        }

        public IEnumerable<Usuario> GetAll(string login, string senha, string cpf = "", string email = "") {            
            if (!string.IsNullOrWhiteSpace(login))
                _parameters.Add(u => u.Email.Equals(login));
            if (!string.IsNullOrWhiteSpace(senha))
                _parameters.Add(u => u.Senha.Equals(senha));
            if (!string.IsNullOrWhiteSpace(cpf))
                _parameters.Add(u => u.CpfCnpj.Equals(cpf));
            if (!string.IsNullOrWhiteSpace(email))
                base._parameters.Add(e => e.Email.Equals(email));

            return base.GetAll();
        }

        public void EnviarEmailRecuperacaoSenha(int id)
        {
            Usuario usuario = this.GetOne(id);
            
            usuario.Senha = new Random().Next().ToString();
            this.Update(usuario);

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"Para acessar o sistema aniamiszinhos utilize a senha {usuario.Senha}. ");
            strBuilder.AppendLine("Não esqueça de alterar sua senha após o login");
            try
            {
                EnviadorEmail.Enviar(usuario.Email, "Recuperação de senha Animaiszinhos", strBuilder.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}