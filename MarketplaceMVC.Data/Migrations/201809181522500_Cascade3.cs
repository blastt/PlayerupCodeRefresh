namespace MarketplaceMVC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cascade3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "BuyerId", "dbo.UserProfiles");
            DropForeignKey("dbo.Orders", "MiddlemanId", "dbo.UserProfiles");
            DropForeignKey("dbo.Orders", "SellerId", "dbo.UserProfiles");
            DropForeignKey("dbo.Dialogs", "CreatorId", "dbo.UserProfiles");
            DropForeignKey("dbo.Dialogs", "CompanionId", "dbo.UserProfiles");
            DropForeignKey("dbo.Feedbacks", "UserToId", "dbo.UserProfiles");
            DropForeignKey("dbo.Feedbacks", "UserFromId", "dbo.UserProfiles");
            DropForeignKey("dbo.Messages", "ReceiverId", "dbo.UserProfiles");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.UserProfiles");
            AddForeignKey("dbo.Orders", "BuyerId", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Orders", "MiddlemanId", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Orders", "SellerId", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Dialogs", "CreatorId", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Dialogs", "CompanionId", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Feedbacks", "UserToId", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Feedbacks", "UserFromId", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Messages", "ReceiverId", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Messages", "SenderId", "dbo.UserProfiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "SenderId", "dbo.UserProfiles");
            DropForeignKey("dbo.Messages", "ReceiverId", "dbo.UserProfiles");
            DropForeignKey("dbo.Feedbacks", "UserFromId", "dbo.UserProfiles");
            DropForeignKey("dbo.Feedbacks", "UserToId", "dbo.UserProfiles");
            DropForeignKey("dbo.Dialogs", "CompanionId", "dbo.UserProfiles");
            DropForeignKey("dbo.Dialogs", "CreatorId", "dbo.UserProfiles");
            DropForeignKey("dbo.Orders", "SellerId", "dbo.UserProfiles");
            DropForeignKey("dbo.Orders", "MiddlemanId", "dbo.UserProfiles");
            DropForeignKey("dbo.Orders", "BuyerId", "dbo.UserProfiles");
            AddForeignKey("dbo.Messages", "SenderId", "dbo.UserProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Messages", "ReceiverId", "dbo.UserProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Feedbacks", "UserFromId", "dbo.UserProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Feedbacks", "UserToId", "dbo.UserProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Dialogs", "CompanionId", "dbo.UserProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Dialogs", "CreatorId", "dbo.UserProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "SellerId", "dbo.UserProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "MiddlemanId", "dbo.UserProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "BuyerId", "dbo.UserProfiles", "Id", cascadeDelete: true);
        }
    }
}
