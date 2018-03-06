namespace MultiPART.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnimalProcedureTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnimalProcedures",
                c => new
                    {
                        AnimalProcedureID = c.Int(nullable: false, identity: true),
                        AnimalID = c.Int(nullable: false),
                        ProcedureID = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Status = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false),
                        LastUpdatedBy = c.Int(nullable: false),
                        LastUpdatedOn = c.DateTimeOffset(nullable: false),
                    })
                .PrimaryKey(t => t.AnimalProcedureID)
                .ForeignKey("dbo.Procedures", t => t.ProcedureID, cascadeDelete: true)
                .ForeignKey("dbo.Animals", t => t.AnimalID, cascadeDelete: true)
                .Index(t => t.ProcedureID)
                .Index(t => t.AnimalID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.AnimalProcedures", new[] { "AnimalID" });
            DropIndex("dbo.AnimalProcedures", new[] { "ProcedureID" });
            DropForeignKey("dbo.AnimalProcedures", "AnimalID", "dbo.Animals");
            DropForeignKey("dbo.AnimalProcedures", "ProcedureID", "dbo.Procedures");
            DropTable("dbo.AnimalProcedures");
        }
    }
}
