namespace MultiPART.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class deletetables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DiseaseModels", "HumanConditionHumanConditionID", "dbo.HumanConditions");
            DropForeignKey("dbo.DiseaseModels", "AnimalConditionAnimalConditionID", "dbo.AnimalConditions");
     //       DropForeignKey("dbo.DiseaseModels", "HumanConditionTypeID", "dbo.Options");
            DropForeignKey("dbo.CommercialDrugComponents", "DrugID", "dbo.Drugs");
            DropForeignKey("dbo.CommercialDrugComponents", "CommercialDrugID", "dbo.CommercialDrugs");
            DropIndex("dbo.DiseaseModels", new[] { "HumanConditionHumanConditionID" });
            DropIndex("dbo.DiseaseModels", new[] { "AnimalConditionAnimalConditionID" });
    //        DropIndex("dbo.DiseaseModels", new[] { "HumanConditionTypeID" });
            DropIndex("dbo.CommercialDrugComponents", new[] { "DrugID" });
            DropIndex("dbo.CommercialDrugComponents", new[] { "CommercialDrugID" });
            DropTable("dbo.DiseaseModels");
            DropTable("dbo.HumanConditions");
            DropTable("dbo.AnimalConditions");
            DropTable("dbo.CommercialDrugs");
            DropTable("dbo.CommercialDrugComponents");
            DropTable("dbo.Drugs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Drugs",
                c => new
                    {
                        DrugID = c.Int(nullable: false, identity: true),
                        DrugName = c.String(),
                        DrugGroup = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.DrugID);
            
            CreateTable(
                "dbo.CommercialDrugComponents",
                c => new
                    {
                        CommercialDrugComponentID = c.Int(nullable: false, identity: true),
                        CommercialDrugID = c.Int(nullable: false),
                        DrugID = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.CommercialDrugComponentID);
            
            CreateTable(
                "dbo.CommercialDrugs",
                c => new
                    {
                        CommercialDrugID = c.Int(nullable: false, identity: true),
                        CommercialDrugName = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.CommercialDrugID);
            
            CreateTable(
                "dbo.AnimalConditions",
                c => new
                    {
                        AnimalConditionID = c.Int(nullable: false, identity: true),
                        AnimalConditionName = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.AnimalConditionID);
            
            CreateTable(
                "dbo.HumanConditions",
                c => new
                    {
                        HumanConditionID = c.Int(nullable: false, identity: true),
                        HumanConditionName = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.HumanConditionID);
            
            CreateTable(
                "dbo.DiseaseModels",
                c => new
                    {
                        DiseaseModelID = c.Int(nullable: false, identity: true),
                        HumanConditionHumanConditionID = c.Int(nullable: false),
                        HumanConditionTypeID = c.Int(nullable: false),
                        AnimalConditionAnimalConditionID = c.Int(nullable: false),
                        DiseaseModelDetail = c.String(),
                        Status = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false),
                        LastUpdatedBy = c.Int(nullable: false),
                        LastUpdatedOn = c.DateTimeOffset(nullable: false),
                    })
                .PrimaryKey(t => t.DiseaseModelID);
            
            CreateIndex("dbo.CommercialDrugComponents", "CommercialDrugID");
            CreateIndex("dbo.CommercialDrugComponents", "DrugID");
     //       CreateIndex("dbo.DiseaseModels", "HumanConditionTypeID");
            CreateIndex("dbo.DiseaseModels", "AnimalConditionAnimalConditionID");
            CreateIndex("dbo.DiseaseModels", "HumanConditionHumanConditionID");
            AddForeignKey("dbo.CommercialDrugComponents", "CommercialDrugID", "dbo.CommercialDrugs", "CommercialDrugID", cascadeDelete: true);
            AddForeignKey("dbo.CommercialDrugComponents", "DrugID", "dbo.Drugs", "DrugID", cascadeDelete: true);
     //       AddForeignKey("dbo.DiseaseModels", "HumanConditionTypeID", "dbo.Options", "OptionID", cascadeDelete: true);
            AddForeignKey("dbo.DiseaseModels", "AnimalConditionAnimalConditionID", "dbo.AnimalConditions", "AnimalConditionID", cascadeDelete: true);
            AddForeignKey("dbo.DiseaseModels", "HumanConditionHumanConditionID", "dbo.HumanConditions", "HumanConditionID", cascadeDelete: true);
        }
    }
}
