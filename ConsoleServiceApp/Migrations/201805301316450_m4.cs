namespace ConsoleServiceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Test", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Test", "Description", c => c.String(nullable: false));
        }
    }
}
