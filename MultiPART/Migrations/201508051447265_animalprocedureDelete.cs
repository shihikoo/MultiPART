namespace MultiPART.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class animalprocedureDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DataEntries", "AnimalProcedureID", "dbo.AnimalProcedures");
            DropForeignKey("dbo.AnimalProcedures", "ProcedureID", "dbo.Procedures");
            DropIndex("dbo.DataEntries", new[] { "AnimalProcedureID" });
            DropIndex("dbo.AnimalProcedures", new[] { "ProcedureID" });
            DropColumn("dbo.DataEntries", "AnimalProcedureID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataEntries", "AnimalProcedureID", c => c.Int(nullable: false));
            CreateIndex("dbo.AnimalProcedures", "ProcedureID");
            CreateIndex("dbo.DataEntries", "AnimalProcedureID");
            AddForeignKey("dbo.AnimalProcedures", "ProcedureID", "dbo.Procedures", "ProcedureID", cascadeDelete: true);
            AddForeignKey("dbo.DataEntries", "AnimalProcedureID", "dbo.AnimalProcedures", "AnimalProcedureID", cascadeDelete: true);
        }
    }
}
