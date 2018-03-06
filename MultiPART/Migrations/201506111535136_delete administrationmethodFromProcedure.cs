namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class deleteadministrationmethodFromProcedure : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Procedures", "AdministrationMethodID", "dbo.Options");
            DropIndex("dbo.Procedures", new[] { "AdministrationMethodID" });
            DropColumn( "dbo.Procedures", "AdministrationMethodID");
           // RenameColumn(table: "dbo.Procedures", name: "AdministrationMethodID", newName: "AdministrationTypeID");
        }
        
        public override void Down()
        {
            AddColumn( "dbo.Procedures", "AdministrationMethodID",c=>c.Int());
            CreateIndex("dbo.Procedures", "AdministrationMethodID");
            AddForeignKey("dbo.Procedures", "AdministrationMethodID", "dbo.Options", "OptionID");
            //RenameColumn(table: "dbo.Procedures", name: "AdministrationTypeID", newName: "AdministrationMethodID");
        }
    }
}
