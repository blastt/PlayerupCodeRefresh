namespace MarketplaceMVC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvatarName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Avatar32", c => c.String());
            AddColumn("dbo.UserProfiles", "Avatar64", c => c.String());
            AddColumn("dbo.UserProfiles", "Avatar96", c => c.String());
            DropColumn("dbo.UserProfiles", "Avatar32Path");
            DropColumn("dbo.UserProfiles", "Avatar48Path");
            DropColumn("dbo.UserProfiles", "Avatar96Path");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "Avatar96Path", c => c.String());
            AddColumn("dbo.UserProfiles", "Avatar48Path", c => c.String());
            AddColumn("dbo.UserProfiles", "Avatar32Path", c => c.String());
            DropColumn("dbo.UserProfiles", "Avatar96");
            DropColumn("dbo.UserProfiles", "Avatar64");
            DropColumn("dbo.UserProfiles", "Avatar32");
        }
    }
}
