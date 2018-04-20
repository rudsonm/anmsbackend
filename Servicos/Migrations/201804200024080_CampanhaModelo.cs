namespace Servicos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampanhaModelo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campanhas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Tipo = c.String(),
                        Meta = c.Single(nullable: false),
                        Contribuicao = c.Single(nullable: false),
                        DataInicio = c.DateTime(nullable: false),
                        DataTermino = c.DateTime(),
                        Descricao = c.String(),
                        Status = c.String(),
                        DataCadastro = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                        DataExclusao = c.DateTime(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Campanhas");
        }
    }
}
