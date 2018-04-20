namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudancaEmFoto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fotoes", "Animal_Id", "dbo.Animals");
            DropIndex("dbo.Fotoes", new[] { "Animal_Id" });
            AddColumn("dbo.Fotoes", "EntidadeNome", c => c.String());
            AddColumn("dbo.Fotoes", "EntidadeId", c => c.Int(nullable: false));
            DropColumn("dbo.Fotoes", "Animal_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fotoes", "Animal_Id", c => c.Int());
            DropColumn("dbo.Fotoes", "EntidadeId");
            DropColumn("dbo.Fotoes", "EntidadeNome");
            CreateIndex("dbo.Fotoes", "Animal_Id");
            AddForeignKey("dbo.Fotoes", "Animal_Id", "dbo.Animals", "Id");
        }
    }
}
