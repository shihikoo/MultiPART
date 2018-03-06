namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class speciesname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Species", "SpeciesName", c => c.String());
            DropColumn("dbo.Species", "SpecieName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Species", "SpecieName", c => c.String());
            DropColumn("dbo.Species", "SpeciesName");
        }
    }
}
