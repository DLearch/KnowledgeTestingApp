namespace ConsoleServiceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Image = c.Binary(),
                        IsCorrect = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invitation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        AddresseeId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        IsTransferable = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.AddresseeId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.SenderId, cascadeDelete: false)
                .ForeignKey("dbo.Test", t => t.TestId, cascadeDelete: true)
                .Index(t => t.SenderId)
                .Index(t => t.AddresseeId)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        RegDate = c.DateTime(nullable: false),
                        EmailIsVisible = c.Boolean(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TestResult",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        Duration = c.Time(nullable: false, precision: 7),
                        Attempts = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Test", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TestId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 150),
                        Description = c.String(nullable: false),
                        Duration = c.Time(precision: 7),
                        Attempts = c.Int(),
                        Interval = c.Time(precision: 7),
                        AddDate = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        RatingSystemId = c.Int(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        IsQuestionsMix = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.RatingSystem", t => t.RatingSystemId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.RatingSystemId);
            
            CreateTable(
                "dbo.RatingSystem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Image = c.Binary(),
                        IsRadio = c.Boolean(nullable: false),
                        IsAnswersMix = c.Boolean(nullable: false),
                        Weight = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invitation", "TestId", "dbo.Test");
            DropForeignKey("dbo.TestResult", "UserId", "dbo.User");
            DropForeignKey("dbo.TestResult", "TestId", "dbo.Test");
            DropForeignKey("dbo.Test", "UserId", "dbo.User");
            DropForeignKey("dbo.Test", "RatingSystemId", "dbo.RatingSystem");
            DropForeignKey("dbo.Test", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Invitation", "SenderId", "dbo.User");
            DropForeignKey("dbo.Invitation", "AddresseeId", "dbo.User");
            DropIndex("dbo.Test", new[] { "RatingSystemId" });
            DropIndex("dbo.Test", new[] { "CategoryId" });
            DropIndex("dbo.Test", new[] { "UserId" });
            DropIndex("dbo.TestResult", new[] { "UserId" });
            DropIndex("dbo.TestResult", new[] { "TestId" });
            DropIndex("dbo.Invitation", new[] { "TestId" });
            DropIndex("dbo.Invitation", new[] { "AddresseeId" });
            DropIndex("dbo.Invitation", new[] { "SenderId" });
            DropTable("dbo.Question");
            DropTable("dbo.RatingSystem");
            DropTable("dbo.Test");
            DropTable("dbo.TestResult");
            DropTable("dbo.User");
            DropTable("dbo.Invitation");
            DropTable("dbo.Category");
            DropTable("dbo.Answer");
        }
    }
}
