namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolicitacaoStatusParaString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SolicitacaoAdocaos", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SolicitacaoAdocaos", "Status", c => c.Int(nullable: false));
        }
    }
}
