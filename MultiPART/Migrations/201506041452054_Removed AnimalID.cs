namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAnimalID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DataEntryDetails", "AnimalAnimalID", "dbo.Animals");
            DropIndex("dbo.DataEntryDetails", new[] { "AnimalAnimalID" });
            DropColumn("dbo.DataEntryDetails", "AnimalAnimalID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataEntryDetails", "AnimalAnimalID", c => c.Int(nullable: false));
            CreateIndex("dbo.DataEntryDetails", "AnimalAnimalID");
            AddForeignKey("dbo.DataEntryDetails", "AnimalAnimalID", "dbo.Animals", "AnimalID", cascadeDelete: true);
        }
    }
}
