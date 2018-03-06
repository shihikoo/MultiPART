namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddDataEntryIdforeignkeyproperty : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DataEntryDetails", name: "DataEntry_DataEntryID", newName: "DataEntryDataEntryID");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.DataEntryDetails", name: "DataEntryDataEntryID", newName: "DataEntry_DataEntryID");
        }
    }
}
