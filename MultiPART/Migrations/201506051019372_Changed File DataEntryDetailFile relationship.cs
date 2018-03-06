namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ChangedFileDataEntryDetailFilerelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataEntries", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DataEntries", "Status");
        }
    }
}
