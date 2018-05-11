namespace SurveySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAnswerModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "AnswerValue", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Answers", "AnswerValue", c => c.String(nullable: false));
        }
    }
}
