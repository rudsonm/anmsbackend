namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoacaoStatusParaString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doacaos", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Doacaos", "Status", c => c.Int(nullable: false));
        }
    }
}
