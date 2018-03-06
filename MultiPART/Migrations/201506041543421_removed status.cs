namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class removedstatus : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DataEntries", "Status");
            DropColumn("dbo.DataEntries", "CreatedBy");
            DropColumn("dbo.DataEntries", "CreatedOn");
            DropColumn("dbo.DataEntries", "LastUpdatedBy");
            DropColumn("dbo.DataEntries", "LastUpdatedOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataEntries", "LastUpdatedOn", c => c.DateTimeOffset(nullable: false));
            AddColumn("dbo.DataEntries", "LastUpdatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.DataEntries", "CreatedOn", c => c.DateTimeOffset(nullable: false));
            AddColumn("dbo.DataEntries", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.DataEntries", "Status", c => c.String());
        }
    }
}
