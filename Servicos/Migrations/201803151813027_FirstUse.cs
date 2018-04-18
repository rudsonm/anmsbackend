namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstUse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Filo = c.Int(nullable: false),
                        Especie = c.String(),
                        Peso = c.Single(nullable: false),
                        Descricao = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                        DataExclusao = c.DateTime(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Tipo = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cidades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Estado_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estadoes", t => t.Estado_Id)
                .Index(t => t.Estado_Id);
            
            CreateTable(
                "dbo.Estadoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Pais_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pais", t => t.Pais_Id)
                .Index(t => t.Pais_Id);
            
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comentarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mensagem = c.String(),
                        DataCadastro = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                        DataExclusao = c.DateTime(),
                        Ativo = c.Boolean(nullable: false),
                        Doacao_Id = c.Int(),
                        Remetente_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doacaos", t => t.Doacao_Id)
                .ForeignKey("dbo.Pessoas", t => t.Remetente_Id)
                .Index(t => t.Doacao_Id)
                .Index(t => t.Remetente_Id);
            
            CreateTable(
                "dbo.Doacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        DataExpiracao = c.DateTime(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                        DataExclusao = c.DateTime(),
                        Ativo = c.Boolean(nullable: false),
                        Animal_Id = c.Int(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animals", t => t.Animal_Id)
                .ForeignKey("dbo.Pessoas", t => t.Usuario_Id)
                .Index(t => t.Animal_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Pessoas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Apelido = c.String(),
                        Email = c.String(),
                        CpfCnpj = c.String(),
                        DataCadastro = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                        DataExclusao = c.DateTime(),
                        Ativo = c.Boolean(nullable: false),
                        Senha = c.String(),
                        SenhaExpirada = c.Boolean(),
                        SuperAdmin = c.Boolean(),
                        NomeFantasia = c.String(),
                        RazaoSocial = c.String(),
                        InscricaoEstadual = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Categoria_Id = c.Int(),
                        Endereco_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.Categoria_Id)
                .ForeignKey("dbo.Enderecoes", t => t.Endereco_Id)
                .Index(t => t.Categoria_Id)
                .Index(t => t.Endereco_Id);
            
            CreateTable(
                "dbo.Enderecoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cep = c.Int(nullable: false),
                        Bairro = c.String(),
                        Logradouro = c.String(),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                        DataExclusao = c.DateTime(),
                        Ativo = c.Boolean(nullable: false),
                        Cidade_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cidades", t => t.Cidade_Id)
                .Index(t => t.Cidade_Id);
            
            CreateTable(
                "dbo.Fotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tipo = c.String(),
                        Bytes = c.Binary(),
                        Ativo = c.Boolean(nullable: false),
                        Animal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animals", t => t.Animal_Id)
                .Index(t => t.Animal_Id);
            
            CreateTable(
                "dbo.SolicitacaoAdocaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Motivo = c.String(),
                        Status = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                        DataExclusao = c.DateTime(),
                        Ativo = c.Boolean(nullable: false),
                        Doacao_Id = c.Int(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doacaos", t => t.Doacao_Id)
                .ForeignKey("dbo.Pessoas", t => t.Usuario_Id)
                .Index(t => t.Doacao_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Telefones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.Int(nullable: false),
                        Tipo = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Pessoa_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoas", t => t.Pessoa_Id)
                .Index(t => t.Pessoa_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Telefones", "Pessoa_Id", "dbo.Pessoas");
            DropForeignKey("dbo.SolicitacaoAdocaos", "Usuario_Id", "dbo.Pessoas");
            DropForeignKey("dbo.SolicitacaoAdocaos", "Doacao_Id", "dbo.Doacaos");
            DropForeignKey("dbo.Pessoas", "Endereco_Id", "dbo.Enderecoes");
            DropForeignKey("dbo.Fotoes", "Animal_Id", "dbo.Animals");
            DropForeignKey("dbo.Pessoas", "Categoria_Id", "dbo.Categorias");
            DropForeignKey("dbo.Comentarios", "Remetente_Id", "dbo.Pessoas");
            DropForeignKey("dbo.Comentarios", "Doacao_Id", "dbo.Doacaos");
            DropForeignKey("dbo.Doacaos", "Usuario_Id", "dbo.Pessoas");
            DropForeignKey("dbo.Enderecoes", "Cidade_Id", "dbo.Cidades");
            DropForeignKey("dbo.Doacaos", "Animal_Id", "dbo.Animals");
            DropForeignKey("dbo.Cidades", "Estado_Id", "dbo.Estadoes");
            DropForeignKey("dbo.Estadoes", "Pais_Id", "dbo.Pais");
            DropIndex("dbo.Telefones", new[] { "Pessoa_Id" });
            DropIndex("dbo.SolicitacaoAdocaos", new[] { "Usuario_Id" });
            DropIndex("dbo.SolicitacaoAdocaos", new[] { "Doacao_Id" });
            DropIndex("dbo.Fotoes", new[] { "Animal_Id" });
            DropIndex("dbo.Enderecoes", new[] { "Cidade_Id" });
            DropIndex("dbo.Pessoas", new[] { "Endereco_Id" });
            DropIndex("dbo.Pessoas", new[] { "Categoria_Id" });
            DropIndex("dbo.Doacaos", new[] { "Usuario_Id" });
            DropIndex("dbo.Doacaos", new[] { "Animal_Id" });
            DropIndex("dbo.Comentarios", new[] { "Remetente_Id" });
            DropIndex("dbo.Comentarios", new[] { "Doacao_Id" });
            DropIndex("dbo.Estadoes", new[] { "Pais_Id" });
            DropIndex("dbo.Cidades", new[] { "Estado_Id" });
            DropTable("dbo.Telefones");
            DropTable("dbo.SolicitacaoAdocaos");
            DropTable("dbo.Fotoes");
            DropTable("dbo.Enderecoes");
            DropTable("dbo.Pessoas");
            DropTable("dbo.Doacaos");
            DropTable("dbo.Comentarios");
            DropTable("dbo.Pais");
            DropTable("dbo.Estadoes");
            DropTable("dbo.Cidades");
            DropTable("dbo.Categorias");
            DropTable("dbo.Animals");
        }
    }
}
