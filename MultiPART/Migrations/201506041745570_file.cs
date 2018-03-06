namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class file : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DataEntryDetailFile", "DataEntryDetailID", "dbo.Files");
            DropIndex("dbo.DataEntryDetailFile", new[] { "DataEntryDetailID" });
            AddColumn("dbo.DataEntryDetailFile", "FileFileID", c => c.Int(nullable: false));
            AlterColumn("dbo.DataEntryDetails", "DataEntryDetailID", c => c.Int(nullable: false, identity: true));
            AddForeignKey("dbo.DataEntryDetailFile", "FileFileID", "dbo.Files", "FileID");
            CreateIndex("dbo.DataEntryDetailFile", "FileFileID");
            DropColumn("dbo.DataEntryDetailFile", "FileID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataEntryDetailFile", "FileID", c => c.Int(nullable: false));
            DropIndex("dbo.DataEntryDetailFile", new[] { "FileFileID" });
            DropForeignKey("dbo.DataEntryDetailFile", "FileFileID", "dbo.Files");
            AlterColumn("dbo.DataEntryDetails", "DataEntryDetailID", c => c.Int(nullable: false));
            DropColumn("dbo.DataEntryDetailFile", "FileFileID");
            CreateIndex("dbo.DataEntryDetailFile", "DataEntryDetailID");
            AddForeignKey("dbo.DataEntryDetailFile", "DataEntryDetailID", "dbo.Files", "FileID");
        }
    }
}
