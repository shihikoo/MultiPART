namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DataEntryDetailFile", "FileFileID", "dbo.Files");
            DropIndex("dbo.DataEntryDetailFile", new[] { "FileFileID" });
            AddColumn("dbo.DataEntryDetailFile", "FileID", c => c.Int(nullable: false));
            AddForeignKey("dbo.DataEntryDetailFile", "FileID", "dbo.Files", "FileID");
            CreateIndex("dbo.DataEntryDetailFile", "FileID");
            DropColumn("dbo.DataEntryDetailFile", "FileFileID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataEntryDetailFile", "FileFileID", c => c.Int(nullable: false));
            DropIndex("dbo.DataEntryDetailFile", new[] { "FileID" });
            DropForeignKey("dbo.DataEntryDetailFile", "FileID", "dbo.Files");
            DropColumn("dbo.DataEntryDetailFile", "FileID");
            CreateIndex("dbo.DataEntryDetailFile", "FileFileID");
            AddForeignKey("dbo.DataEntryDetailFile", "FileFileID", "dbo.Files", "FileID");
        }
    }
}
