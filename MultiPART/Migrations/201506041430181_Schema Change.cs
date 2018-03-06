namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SchemaChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DataEntryDetails", "DataEntryDesignDataEntryDesignID", "dbo.DataEntryDesigns");
            DropIndex("dbo.DataEntryDetails", new[] { "DataEntryDesignDataEntryDesignID" });
            CreateTable(
                "dbo.DataEntries",
                c => new
                    {
                        DataEntryID = c.Int(nullable: false, identity: true),
                        AnimalAnimalID = c.Int(nullable: false),
                        DataEntryDesignDataEntryDesignID = c.Int(nullable: false),
                        Status = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false),
                        LastUpdatedBy = c.Int(nullable: false),
                        LastUpdatedOn = c.DateTimeOffset(nullable: false),
                    })
                .PrimaryKey(t => t.DataEntryID)
                .ForeignKey("dbo.DataEntryDesigns", t => t.DataEntryDesignDataEntryDesignID)
                .ForeignKey("dbo.Animals", t => t.AnimalAnimalID)
                .Index(t => t.DataEntryDesignDataEntryDesignID)
                .Index(t => t.AnimalAnimalID);
            
            AddColumn("dbo.DataEntryDetails", "DataEntry_DataEntryID", c => c.Int(nullable: false));
            AddForeignKey("dbo.DataEntryDetails", "DataEntry_DataEntryID", "dbo.DataEntries", "DataEntryID");
            CreateIndex("dbo.DataEntryDetails", "DataEntry_DataEntryID");
            DropColumn("dbo.DataEntryDetails", "DataEntryDesignDataEntryDesignID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataEntryDetails", "DataEntryDesignDataEntryDesignID", c => c.Int(nullable: false));
            DropIndex("dbo.DataEntries", new[] { "AnimalAnimalID" });
            DropIndex("dbo.DataEntries", new[] { "DataEntryDesignDataEntryDesignID" });
            DropIndex("dbo.DataEntryDetails", new[] { "DataEntry_DataEntryID" });
            DropForeignKey("dbo.DataEntries", "AnimalAnimalID", "dbo.Animals");
            DropForeignKey("dbo.DataEntries", "DataEntryDesignDataEntryDesignID", "dbo.DataEntryDesigns");
            DropForeignKey("dbo.DataEntryDetails", "DataEntry_DataEntryID", "dbo.DataEntries");
            DropColumn("dbo.DataEntryDetails", "DataEntry_DataEntryID");
            DropTable("dbo.DataEntries");
            CreateIndex("dbo.DataEntryDetails", "DataEntryDesignDataEntryDesignID");
            AddForeignKey("dbo.DataEntryDetails", "DataEntryDesignDataEntryDesignID", "dbo.DataEntryDesigns", "DataEntryDesignID");
        }
    }
}
