namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Merge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parecers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Doacao_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doacaos", t => t.Doacao_Id)
                .Index(t => t.Doacao_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parecers", "Doacao_Id", "dbo.Doacaos");
            DropIndex("dbo.Parecers", new[] { "Doacao_Id" });
            DropTable("dbo.Parecers");
        }
    }
}
