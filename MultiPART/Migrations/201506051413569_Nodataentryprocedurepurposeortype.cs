namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Nodataentryprocedurepurposeortype : DbMigration
    {
        public override void Up()
        {
      //      DropForeignKey("dbo.DataEntryFields", "ProcedurePurposeOrTypeID", "dbo.Options");
       //     DropIndex("dbo.DataEntryFields", new[] { "ProcedurePurposeOrTypeID" });
            DropColumn("dbo.DataEntryFields", "ProcedurePurposeOrTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataEntryFields", "ProcedurePurposeOrTypeID", c => c.Int(nullable: false));
      //      CreateIndex("dbo.DataEntryFields", "ProcedurePurposeOrTypeID");
      //      AddForeignKey("dbo.DataEntryFields", "ProcedurePurposeOrTypeID", "dbo.Options", "OptionID", cascadeDelete: true);
        }
    }
}
