namespace MultiPART.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class review : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        SectionNumber = c.String(),
                        Content = c.String(),
                        Instruction = c.String(),
                        ChecklistID = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.ProcedureDetails", t => t.ChecklistID, cascadeDelete: true)
                .Index(t => t.ChecklistID);
            
            CreateTable(
                "dbo.BehavioralScores",
                c => new
                    {
                        BehavioralScoreID = c.Int(nullable: false, identity: true),
                        QuestionID = c.Int(nullable: false),
                        UserDataentryAssignmentID = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        Status = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        LastUpdatedBy = c.Int(nullable: false),
                        LastUpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.BehavioralScoreID)
                .ForeignKey("dbo.Questions", t => t.QuestionID, cascadeDelete: true)
                .ForeignKey("dbo.UserDataentryAssignments", t => t.UserDataentryAssignmentID, cascadeDelete: true)
                .Index(t => t.QuestionID)
                .Index(t => t.UserDataentryAssignmentID);
            
            CreateTable(
                "dbo.UserDataentryAssignments",
                c => new
                    {
                        UserDataentryAssignmentID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        DataEntryID = c.Int(nullable: false),
                        Status = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        LastUpdatedBy = c.Int(nullable: false),
                        LastUpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.UserDataentryAssignmentID)
                .ForeignKey("dbo.DataEntries", t => t.DataEntryID)
                .ForeignKey("dbo.UserProfile", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.DataEntryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "ChecklistID", "dbo.ProcedureDetails");
            DropForeignKey("dbo.BehavioralScores", "UserDataentryAssignmentID", "dbo.UserDataentryAssignments");
            DropForeignKey("dbo.UserDataentryAssignments", "UserID", "dbo.UserProfile");
            DropForeignKey("dbo.UserDataentryAssignments", "DataEntryID", "dbo.DataEntries");
            DropForeignKey("dbo.BehavioralScores", "QuestionID", "dbo.Questions");
            DropIndex("dbo.UserDataentryAssignments", new[] { "DataEntryID" });
            DropIndex("dbo.UserDataentryAssignments", new[] { "UserID" });
            DropIndex("dbo.BehavioralScores", new[] { "UserDataentryAssignmentID" });
            DropIndex("dbo.BehavioralScores", new[] { "QuestionID" });
            DropIndex("dbo.Questions", new[] { "ChecklistID" });
            DropTable("dbo.UserDataentryAssignments");
            DropTable("dbo.BehavioralScores");
            DropTable("dbo.Questions");
        }
    }
}
