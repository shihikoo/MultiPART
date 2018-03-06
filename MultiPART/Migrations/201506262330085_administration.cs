namespace MultiPART.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class administration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrations",
                c => new
                    {
                        AdministrationID = c.Int(nullable: false, identity: true),
                        ProcedureID = c.Int(nullable: false),
                        StartTime = c.DateTimeOffset(nullable: false),
                        EndTime = c.DateTimeOffset(),
                        Status = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false),
                        LastUpdatedBy = c.Int(nullable: false),
                        LastUpdatedOn = c.DateTimeOffset(nullable: false),
                    })
                .PrimaryKey(t => t.AdministrationID)
                .ForeignKey("dbo.Procedures", t => t.ProcedureID)
                .Index(t => t.ProcedureID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Administrations", new[] { "ProcedureID" });
            DropForeignKey("dbo.Administrations", "ProcedureID", "dbo.Procedures");
            DropTable("dbo.Administrations");
        }
    }
}
