namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class units : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SIUnits",
                c => new
                    {
                        SIUnitID = c.Int(nullable: false, identity: true),
                        QuantityName = c.String(),
                        UnitName = c.String(),
                        UnitSymbol = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.SIUnitID);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitID = c.Int(nullable: false, identity: true),
                        UnitName = c.String(),
                        UnitSymbol = c.String(),
                        ConversionFactor = c.Int(nullable: false),
                        SIID = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.UnitID)
                .ForeignKey("dbo.SIUnits", t => t.SIID)
                .Index(t => t.SIID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Units", new[] { "SIID" });
            DropForeignKey("dbo.Units", "SIID", "dbo.SIUnits");
            DropTable("dbo.Units");
            DropTable("dbo.SIUnits");
        }
    }
}
