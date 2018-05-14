namespace SurveySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldsToCustomerSurveyResponse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerSurveyResponses", "SurveyName", c => c.String(nullable: false));
            AddColumn("dbo.CustomerSurveyResponses", "QuestionName", c => c.String(nullable: false));
            AddColumn("dbo.CustomerSurveyResponses", "AnswerValue", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerSurveyResponses", "AnswerValue");
            DropColumn("dbo.CustomerSurveyResponses", "QuestionName");
            DropColumn("dbo.CustomerSurveyResponses", "SurveyName");
        }
    }
}
