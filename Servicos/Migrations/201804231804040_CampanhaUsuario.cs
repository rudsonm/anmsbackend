namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampanhaUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Campanhas", "Usuario_Id", c => c.Int());
            CreateIndex("dbo.Campanhas", "Usuario_Id");
            AddForeignKey("dbo.Campanhas", "Usuario_Id", "dbo.Pessoas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Campanhas", "Usuario_Id", "dbo.Pessoas");
            DropIndex("dbo.Campanhas", new[] { "Usuario_Id" });
            DropColumn("dbo.Campanhas", "Usuario_Id");
        }
    }
}
