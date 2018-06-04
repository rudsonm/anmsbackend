namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParecerCampanha : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Parecers", "Doacao_Id", "dbo.Doacaos");
            DropIndex("dbo.Parecers", new[] { "Doacao_Id" });
            AddColumn("dbo.Parecers", "Campanha_Id", c => c.Int());
            CreateIndex("dbo.Parecers", "Campanha_Id");
            AddForeignKey("dbo.Parecers", "Campanha_Id", "dbo.Campanhas", "Id");
            DropColumn("dbo.Parecers", "Doacao_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parecers", "Doacao_Id", c => c.Int());
            DropForeignKey("dbo.Parecers", "Campanha_Id", "dbo.Campanhas");
            DropIndex("dbo.Parecers", new[] { "Campanha_Id" });
            DropColumn("dbo.Parecers", "Campanha_Id");
            CreateIndex("dbo.Parecers", "Doacao_Id");
            AddForeignKey("dbo.Parecers", "Doacao_Id", "dbo.Doacaos", "Id");
        }
    }
}
