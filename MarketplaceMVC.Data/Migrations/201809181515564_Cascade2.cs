namespace MarketplaceMVC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cascade2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.Users");
            AddForeignKey("dbo.UserProfiles", "Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.Users");
            AddForeignKey("dbo.UserProfiles", "Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
