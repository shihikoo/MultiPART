namespace MultiPART.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnimalProcedureRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DataEntryDetails", "DataEntryDataEntryID", "dbo.DataEntries");
            DropForeignKey("dbo.DataEntries", "AnimalAnimalID", "dbo.Animals");
            DropIndex("dbo.DataEntryDetails", new[] { "DataEntryDataEntryID" });
            DropIndex("dbo.DataEntries", new[] { "AnimalAnimalID" });
            AddColumn("dbo.DataEntries", "AnimalProcedureID", c => c.Int(nullable: false));
            AddForeignKey("dbo.DataEntryDetails", "DataEntryDataEntryID", "dbo.DataEntries", "DataEntryID", cascadeDelete: true);
            AddForeignKey("dbo.DataEntries", "AnimalProcedureID", "dbo.AnimalProcedures", "AnimalProcedureID", cascadeDelete: true);
            CreateIndex("dbo.DataEntryDetails", "DataEntryDataEntryID");
            CreateIndex("dbo.DataEntries", "AnimalProcedureID");
            DropColumn("dbo.DataEntries", "AnimalAnimalID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataEntries", "AnimalAnimalID", c => c.Int(nullable: false));
            DropIndex("dbo.DataEntries", new[] { "AnimalProcedureID" });
            DropIndex("dbo.DataEntryDetails", new[] { "DataEntryDataEntryID" });
            DropForeignKey("dbo.DataEntries", "AnimalProcedureID", "dbo.AnimalProcedures");
            DropForeignKey("dbo.DataEntryDetails", "DataEntryDataEntryID", "dbo.DataEntries");
            DropColumn("dbo.DataEntries", "AnimalProcedureID");
            CreateIndex("dbo.DataEntries", "AnimalAnimalID");
            CreateIndex("dbo.DataEntryDetails", "DataEntryDataEntryID");
            AddForeignKey("dbo.DataEntries", "AnimalAnimalID", "dbo.Animals", "AnimalID");
            AddForeignKey("dbo.DataEntryDetails", "DataEntryDataEntryID", "dbo.DataEntries", "DataEntryID");
        }
    }
}
