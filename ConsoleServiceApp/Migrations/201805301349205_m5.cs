namespace ConsoleServiceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Answer", "QuestionId");
            CreateIndex("dbo.Question", "TestId");
            AddForeignKey("dbo.Answer", "QuestionId", "dbo.Question", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Question", "TestId", "dbo.Test", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Question", "TestId", "dbo.Test");
            DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
            DropIndex("dbo.Question", new[] { "TestId" });
            DropIndex("dbo.Answer", new[] { "QuestionId" });
        }
    }
}
