namespace MultiPART.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteStartTimeInProject : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Procedures", "StartTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Procedures", "StartTime", c => c.Single(nullable: false));
        }
    }
}
