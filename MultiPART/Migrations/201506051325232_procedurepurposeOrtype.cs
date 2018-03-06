namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class procedurepurposeOrtype : DbMigration
    {
        public override void Up()
        {
          //  RenameColumn(table: "dbo.ProcedureDetailOptionFields", name: "ProcedureDetailFieldTypeID", newName: "ProcedurePurposeOrTypeID");
            AddColumn("dbo.DataEntryFields", "ProcedurePurposeOrTypeID", c => c.Int(nullable: false));

        //    AddForeignKey("dbo.ProcedureDetailOptionFields", "ProcedureDetailFieldTypeID", "dbo.Options", "OptionID", cascadeDelete: true);
            
  //          AddForeignKey("dbo.DataEntryFields", "ProcedurePurposeOrTypeID", "dbo.Options", "OptionID", cascadeDelete: true);

  //          CreateIndex("dbo.ProcedureDetailOptionFields", "ProcedureDetailFieldTypeID");
          
 //           CreateIndex("dbo.DataEntryFields", "ProcedurePurposeOrTypeID");
        }
        
        public override void Down()
        {
 //           DropIndex("dbo.DataEntryFields", new[] { "ProcedurePurposeOrTypeID" });
     //       DropIndex("dbo.ProcedureDetailOptionFields", new[] { "ProcedureDetailFieldTypeID" });
 //           DropForeignKey("dbo.DataEntryFields", "ProcedurePurposeOrTypeID", "dbo.Options");
       //     DropForeignKey("dbo.ProcedureDetailOptionFields", "ProcedureDetailFieldTypeID", "dbo.Options");
            DropColumn("dbo.DataEntryFields", "ProcedurePurposeOrTypeID");
        //    RenameColumn(table: "dbo.ProcedureDetailOptionFields", name: "ProcedurePurposeOrTypeID", newName: "ProcedureDetailFieldTypeID");
        }
    }
}
