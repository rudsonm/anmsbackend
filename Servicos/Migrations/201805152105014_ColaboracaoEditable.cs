namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColaboracaoEditable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Colaboracaos", "DataCadastro", c => c.DateTime(nullable: false));
            AddColumn("dbo.Colaboracaos", "DataModificacao", c => c.DateTime());
            AddColumn("dbo.Colaboracaos", "DataExclusao", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Colaboracaos", "DataExclusao");
            DropColumn("dbo.Colaboracaos", "DataModificacao");
            DropColumn("dbo.Colaboracaos", "DataCadastro");
        }
    }
}
