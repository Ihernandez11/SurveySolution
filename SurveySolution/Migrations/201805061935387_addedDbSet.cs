namespace SurveySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDbSet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerValue = c.String(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerSurveyResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        SurveyId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        AnswerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionName = c.String(nullable: false),
                        QuestionType = c.String(nullable: false),
                        Required = c.Boolean(nullable: false),
                        SurveyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId);
            
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyTitle = c.String(nullable: false),
                        SurveyDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Questions", new[] { "SurveyId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.Surveys");
            DropTable("dbo.Questions");
            DropTable("dbo.CustomerSurveyResponses");
            DropTable("dbo.Customers");
            DropTable("dbo.Answers");
            DropTable("dbo.Admins");
        }
    }
}
