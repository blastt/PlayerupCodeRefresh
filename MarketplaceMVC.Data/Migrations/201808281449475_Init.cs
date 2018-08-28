namespace MarketplaceMVC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountInfos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        EmailPassword = c.String(),
                        AdditionalInformation = c.String(),
                        OrderId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        BuyerFeedbacked = c.Boolean(nullable: false),
                        SellerFeedbacked = c.Boolean(nullable: false),
                        BuyerChecked = c.Boolean(nullable: false),
                        SellerChecked = c.Boolean(nullable: false),
                        JobId = c.String(),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WithmiddlemanSum = c.Decimal(precision: 18, scale: 2),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        WithdrawAmount = c.Decimal(precision: 18, scale: 2),
                        AmmountSellerGet = c.Decimal(precision: 18, scale: 2),
                        WithdrawAmountSellerGet = c.Decimal(precision: 18, scale: 2),
                        CurrentStatusId = c.Int(nullable: false),
                        MiddlemanId = c.Int(),
                        BuyerId = c.Int(),
                        SellerId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.BuyerId)
                .ForeignKey("dbo.UserProfiles", t => t.MiddlemanId)
                .ForeignKey("dbo.UserProfiles", t => t.SellerId)
                .ForeignKey("dbo.OrderStatuses", t => t.CurrentStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Offers", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.CurrentStatusId)
                .Index(t => t.MiddlemanId)
                .Index(t => t.BuyerId)
                .Index(t => t.SellerId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Avatar32Path = c.String(),
                        Avatar48Path = c.String(),
                        Avatar96Path = c.String(),
                        PositiveFeedbackCount = c.Int(nullable: false),
                        NegativeFeedbackCount = c.Int(nullable: false),
                        Rating = c.Int(),
                        AllFeedbackCount = c.Int(),
                        PositiveFeedbackProcent = c.Double(),
                        NegativeFeedbackProcent = c.Double(),
                        SuccessOrderRate = c.Int(),
                        Discription = c.String(),
                        IsOnline = c.Boolean(nullable: false),
                        LockoutReason = c.String(),
                        Name = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Billings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatorId = c.Int(nullable: false),
                        CompanionId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.CreatorId)
                .ForeignKey("dbo.UserProfiles", t => t.CompanionId)
                .Index(t => t.CreatorId)
                .Index(t => t.CompanionId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageBody = c.String(nullable: false, maxLength: 200),
                        FromViewed = c.Boolean(nullable: false),
                        ToViewed = c.Boolean(nullable: false),
                        SenderDeleted = c.Boolean(nullable: false),
                        ReceiverDeleted = c.Boolean(nullable: false),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        DialogId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dialogs", t => t.DialogId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.ReceiverId)
                .ForeignKey("dbo.UserProfiles", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId)
                .Index(t => t.DialogId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Grade = c.Int(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 50),
                        UserToId = c.Int(nullable: false),
                        UserFromId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Order_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.UserToId)
                .ForeignKey("dbo.UserProfiles", t => t.UserFromId)
                .Index(t => t.UserToId)
                .Index(t => t.UserFromId)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false, maxLength: 100),
                        Discription = c.String(nullable: false, maxLength: 1000),
                        AccountLogin = c.String(nullable: false, maxLength: 50),
                        PersonalAccount = c.Boolean(nullable: false),
                        CountOfGames = c.Int(nullable: false),
                        CreatedAccountDate = c.DateTime(),
                        SteamLevel = c.Int(nullable: false),
                        IsBanned = c.Boolean(nullable: false),
                        Url = c.String(),
                        JobId = c.String(),
                        State = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Views = c.Int(nullable: false),
                        SellerPaysMiddleman = c.Boolean(nullable: false),
                        MiddlemanPrice = c.Decimal(precision: 18, scale: 2),
                        DateDeleted = c.DateTime(),
                        GameId = c.Int(nullable: false),
                        UserProfileId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Value = c.String(nullable: false),
                        ImagePath = c.String(),
                        Rank = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScreenshotPathes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        OfferId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Offers", t => t.OfferId, cascadeDelete: true)
                .Index(t => t.OfferId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.ReceiverId)
                .ForeignKey("dbo.UserProfiles", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Withdraws",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaywayName = c.String(),
                        Details = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPaid = c.Boolean(nullable: false),
                        IsCanceled = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OrderStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DuringName = c.String(),
                        FinishedName = c.String(),
                        Value = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StatusLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        OldStatusId = c.Int(nullable: false),
                        NewStatusId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderStatuses", t => t.NewStatusId)
                .ForeignKey("dbo.OrderStatuses", t => t.OldStatusId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.OldStatusId)
                .Index(t => t.NewStatusId);
            
            AddColumn("dbo.Roles", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserClaims", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserLogins", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.UserLogins", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserRoles", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.UserRoles", "CreatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Users", "CreateDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "CreateDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.AccountInfos", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Id", "dbo.Offers");
            DropForeignKey("dbo.Orders", "CurrentStatusId", "dbo.OrderStatuses");
            DropForeignKey("dbo.StatusLogs", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.StatusLogs", "OldStatusId", "dbo.OrderStatuses");
            DropForeignKey("dbo.StatusLogs", "NewStatusId", "dbo.OrderStatuses");
            DropForeignKey("dbo.Withdraws", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.Users");
            DropForeignKey("dbo.Transactions", "SenderId", "dbo.UserProfiles");
            DropForeignKey("dbo.Transactions", "ReceiverId", "dbo.UserProfiles");
            DropForeignKey("dbo.Transactions", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "SellerId", "dbo.UserProfiles");
            DropForeignKey("dbo.Orders", "MiddlemanId", "dbo.UserProfiles");
            DropForeignKey("dbo.Orders", "BuyerId", "dbo.UserProfiles");
            DropForeignKey("dbo.Offers", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.ScreenshotPathes", "OfferId", "dbo.Offers");
            DropForeignKey("dbo.Offers", "GameId", "dbo.Games");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.UserProfiles");
            DropForeignKey("dbo.Messages", "ReceiverId", "dbo.UserProfiles");
            DropForeignKey("dbo.Feedbacks", "UserFromId", "dbo.UserProfiles");
            DropForeignKey("dbo.Feedbacks", "UserToId", "dbo.UserProfiles");
            DropForeignKey("dbo.Feedbacks", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Dialogs", "CompanionId", "dbo.UserProfiles");
            DropForeignKey("dbo.Dialogs", "CreatorId", "dbo.UserProfiles");
            DropForeignKey("dbo.Messages", "DialogId", "dbo.Dialogs");
            DropForeignKey("dbo.Billings", "UserId", "dbo.UserProfiles");
            DropIndex("dbo.StatusLogs", new[] { "NewStatusId" });
            DropIndex("dbo.StatusLogs", new[] { "OldStatusId" });
            DropIndex("dbo.StatusLogs", new[] { "OrderId" });
            DropIndex("dbo.Withdraws", new[] { "UserId" });
            DropIndex("dbo.Transactions", new[] { "OrderId" });
            DropIndex("dbo.Transactions", new[] { "ReceiverId" });
            DropIndex("dbo.Transactions", new[] { "SenderId" });
            DropIndex("dbo.ScreenshotPathes", new[] { "OfferId" });
            DropIndex("dbo.Offers", new[] { "UserProfileId" });
            DropIndex("dbo.Offers", new[] { "GameId" });
            DropIndex("dbo.Feedbacks", new[] { "Order_Id" });
            DropIndex("dbo.Feedbacks", new[] { "UserFromId" });
            DropIndex("dbo.Feedbacks", new[] { "UserToId" });
            DropIndex("dbo.Messages", new[] { "DialogId" });
            DropIndex("dbo.Messages", new[] { "ReceiverId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.Dialogs", new[] { "CompanionId" });
            DropIndex("dbo.Dialogs", new[] { "CreatorId" });
            DropIndex("dbo.Billings", new[] { "UserId" });
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.Orders", new[] { "SellerId" });
            DropIndex("dbo.Orders", new[] { "BuyerId" });
            DropIndex("dbo.Orders", new[] { "MiddlemanId" });
            DropIndex("dbo.Orders", new[] { "CurrentStatusId" });
            DropIndex("dbo.Orders", new[] { "Id" });
            DropIndex("dbo.AccountInfos", new[] { "OrderId" });
            DropColumn("dbo.UserRoles", "CreatedDate");
            DropColumn("dbo.UserRoles", "Id");
            DropColumn("dbo.UserLogins", "CreatedDate");
            DropColumn("dbo.UserLogins", "Id");
            DropColumn("dbo.Users", "CreatedDate");
            DropColumn("dbo.UserClaims", "CreatedDate");
            DropColumn("dbo.Roles", "CreatedDate");
            DropTable("dbo.StatusLogs");
            DropTable("dbo.OrderStatuses");
            DropTable("dbo.Withdraws");
            DropTable("dbo.Transactions");
            DropTable("dbo.ScreenshotPathes");
            DropTable("dbo.Games");
            DropTable("dbo.Offers");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Messages");
            DropTable("dbo.Dialogs");
            DropTable("dbo.Billings");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Orders");
            DropTable("dbo.AccountInfos");
        }
    }
}
