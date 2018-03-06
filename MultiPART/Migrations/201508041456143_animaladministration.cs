namespace MultiPART.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class animaladministration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Procedures", "AdministrationTypeID", "dbo.Options");
            DropIndex("dbo.Procedures", new[] { "AdministrationTypeID" });
            DropColumn(table: "dbo.Procedures", name: "AdministrationTypeID");
            CreateTable(
                "dbo.AnimalAdministrations",
                c => new
                    {
                        AnimalAdministrationID = c.Int(nullable: false, identity: true),
                        AnimalID = c.Int(nullable: false),
                        AdministrationID = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Status = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false),
                        LastUpdatedBy = c.Int(nullable: false),
                        LastUpdatedOn = c.DateTimeOffset(nullable: false),
                    })
                .PrimaryKey(t => t.AnimalAdministrationID)
                .ForeignKey("dbo.Administrations", t => t.AdministrationID, cascadeDelete: true)
                .ForeignKey("dbo.Animals", t => t.AnimalID, cascadeDelete: true)
                .Index(t => t.AdministrationID)
                .Index(t => t.AnimalID);
            
            AddColumn("dbo.DataEntries", "AnimalAdministration_AnimalAdministrationID", c => c.Int());
            AddForeignKey("dbo.DataEntries", "AnimalAdministration_AnimalAdministrationID", "dbo.AnimalAdministrations", "AnimalAdministrationID");
            CreateIndex("dbo.DataEntries", "AnimalAdministration_AnimalAdministrationID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AnimalAdministrations", new[] { "AnimalID" });
            DropIndex("dbo.AnimalAdministrations", new[] { "AdministrationID" });
            DropIndex("dbo.DataEntries", new[] { "AnimalAdministration_AnimalAdministrationID" });
            DropForeignKey("dbo.AnimalAdministrations", "AnimalID", "dbo.Animals");
            DropForeignKey("dbo.AnimalAdministrations", "AdministrationID", "dbo.Administrations");
            DropForeignKey("dbo.DataEntries", "AnimalAdministration_AnimalAdministrationID", "dbo.AnimalAdministrations");
            DropColumn("dbo.DataEntries", "AnimalAdministration_AnimalAdministrationID");
            DropTable("dbo.AnimalAdministrations");

            AddColumn("dbo.Procedures", "AdministrationTypeID", c => c.Int());
            CreateIndex("dbo.Procedures", "AdministrationTypeID");
            AddForeignKey("dbo.Procedures", "AdministrationTypeID", "dbo.Options", "OptionID", cascadeDelete: true);
        }
    }
}
