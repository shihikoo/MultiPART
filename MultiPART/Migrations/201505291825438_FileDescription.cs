namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class FileDescription : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DataEntryDesigns", "ProcedureProcedureID", "dbo.Procedures");
            DropIndex("dbo.DataEntryDesigns", new[] { "ProcedureProcedureID" });
            AddColumn("dbo.Files", "Description", c => c.String());
            AddForeignKey("dbo.DataEntryDesigns", "ProcedureProcedureID", "dbo.Procedures", "ProcedureID", cascadeDelete: true);
            CreateIndex("dbo.DataEntryDesigns", "ProcedureProcedureID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DataEntryDesigns", new[] { "ProcedureProcedureID" });
            DropForeignKey("dbo.DataEntryDesigns", "ProcedureProcedureID", "dbo.Procedures");
            DropColumn("dbo.Files", "Description");
            CreateIndex("dbo.DataEntryDesigns", "ProcedureProcedureID");
            AddForeignKey("dbo.DataEntryDesigns", "ProcedureProcedureID", "dbo.Procedures", "ProcedureID");
        }
    }
}
