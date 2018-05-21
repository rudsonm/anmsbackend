namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColaboracaoQuantidadeFloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Colaboracaos", "Quantidade", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Colaboracaos", "Quantidade", c => c.String());
        }
    }
}
