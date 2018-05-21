namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Colaboracao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colaboracaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Campanha_Id = c.Int(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Campanhas", t => t.Campanha_Id)
                .ForeignKey("dbo.Pessoas", t => t.Usuario_Id)
                .Index(t => t.Campanha_Id)
                .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Colaboracaos", "Usuario_Id", "dbo.Pessoas");
            DropForeignKey("dbo.Colaboracaos", "Campanha_Id", "dbo.Campanhas");
            DropIndex("dbo.Colaboracaos", new[] { "Usuario_Id" });
            DropIndex("dbo.Colaboracaos", new[] { "Campanha_Id" });
            DropTable("dbo.Colaboracaos");
        }
    }
}
