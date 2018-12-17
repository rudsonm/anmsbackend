namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrationinicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnimalFotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Animal = c.Int(nullable: false),
                        Bytes = c.Binary(),
                        MimeType = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CampanhaFotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Campanha = c.Int(nullable: false),
                        Bytes = c.Binary(),
                        MimeType = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParecerFotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Parecer = c.Int(nullable: false),
                        Bytes = c.Binary(),
                        MimeType = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PessoaFotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pessoa = c.Int(nullable: false),
                        Bytes = c.Binary(),
                        MimeType = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Fotoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Fotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntidadeNome = c.String(),
                        EntidadeId = c.Int(nullable: false),
                        Tipo = c.String(),
                        Bytes = c.Binary(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.PessoaFotoes");
            DropTable("dbo.ParecerFotoes");
            DropTable("dbo.CampanhaFotoes");
            DropTable("dbo.AnimalFotoes");
        }
    }
}
