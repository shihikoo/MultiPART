namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class procedurepurposeOrtype1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataEntryFields", "ProcedurePurposeOrTypeID", c => c.Int(nullable: false));


     //                 AddForeignKey("dbo.DataEntryFields", "ProcedurePurposeOrTypeID", "dbo.Options", "OptionID", cascadeDelete: true);

            CreateIndex("dbo.ProcedureDetailOptionFields", "ProcedurePurposeOrTypeID");

        //              CreateIndex("dbo.DataEntryFields", "ProcedurePurposeOrTypeID");
        }
        
        public override void Down()
        {
   //        DropIndex("dbo.DataEntryFields", new[] { "ProcedurePurposeOrTypeID" });
            DropIndex("dbo.ProcedureDetailOptionFields", new[] { "ProcedurePurposeOrTypeID" });
     //                  DropForeignKey("dbo.DataEntryFields", "ProcedurePurposeOrTypeID", "dbo.Options");

            DropColumn("dbo.DataEntryFields", "ProcedurePurposeOrTypeID");
        }
    }
}
