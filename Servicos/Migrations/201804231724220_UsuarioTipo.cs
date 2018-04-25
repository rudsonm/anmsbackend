namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsuarioTipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoas", "Tipo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pessoas", "Tipo");
        }
    }
}
