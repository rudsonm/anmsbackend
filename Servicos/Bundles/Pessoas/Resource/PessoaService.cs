using Servicos.Bundles.Core.Repository;
using Servicos.Bundles.Core.Resource;
using Servicos.Bundles.Pessoas.Entity;
using Servicos.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Servicos.Bundles.Pessoas.Resource
{
    public class PessoaService : AbstractService<Pessoa>
    {
        public PessoaService(IRepository repository)
            : base(repository)
        {

        }

        public IEnumerable<Pessoa> GetAll(string nome, string cpfCnpj, string email)
        {
            if(!string.IsNullOrWhiteSpace(nome))
                base._parameters.Add(e => e.Nome.Contains(nome));

            if(!string.IsNullOrWhiteSpace(cpfCnpj))
                base._parameters.Add(e => e.CpfCnpj.Equals(cpfCnpj));

            if (!string.IsNullOrWhiteSpace(email))
                base._parameters.Add(e => e.Email.Equals(email));

            return base.GetAll();
        }

        public override void BeforeCreate(Pessoa pessoa)
        {
            if (pessoa.CpfCnpj.Length == 11 && !PessoaService.validarCpf(pessoa.CpfCnpj))
                throw new FormatException("CPF inválido");
        }

        public override void BeforeUpdate(Pessoa pessoa)
        {
            if (pessoa.CpfCnpj.Length == 11 && !PessoaService.validarCpf(pessoa.CpfCnpj))
                throw new FormatException("CPF inválido");
        }

        public static bool validarCpf(string cpf)
        {
            int soma = 0, resto, tmp;
            for (int i = 10; i > 1; i--)
                soma += i * int.Parse(cpf.Substring(10-i, 1));
            resto = (soma * 10) % 11;
            int primeiroDigito = (resto == 10) ? 0 : resto;
            tmp = int.Parse(cpf.Substring(9, 1));
            if (primeiroDigito != tmp)
                return false;

            soma = 0;
            for (int i = 11; i > 2; i--)
                soma += i * int.Parse(cpf.Substring(11 - i, 1));
            resto = (soma * 10) % 11;
            int segundoDigito = (resto == 10) ? 0 : resto;
            tmp = int.Parse(cpf.Substring(10, 1));
            if (segundoDigito != tmp)
                return false;

            return true;
        }
    }
}