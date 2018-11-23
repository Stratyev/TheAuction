namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bets",
                c => new
                    {
                        Bet_id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Customer_Customer_id = c.Int(),
                        Lot_Lot_id = c.Int(),
                    })
                .PrimaryKey(t => t.Bet_id)
                .ForeignKey("dbo.Customers", t => t.Customer_Customer_id)
                .ForeignKey("dbo.Lots", t => t.Lot_Lot_id)
                .Index(t => t.Customer_Customer_id)
                .Index(t => t.Lot_Lot_id);
            
            CreateTable(
                "dbo.Checks",
                c => new
                    {
                        Check_id = c.Int(nullable: false, identity: true),
                        Cost = c.Double(nullable: false),
                        Status = c.String(),
                        Bet_Bet_id = c.Int(),
                        DeliveryOption_DeliveryOption_id = c.Int(),
                    })
                .PrimaryKey(t => t.Check_id)
                .ForeignKey("dbo.Bets", t => t.Bet_Bet_id)
                .ForeignKey("dbo.DeliveryOptions", t => t.DeliveryOption_DeliveryOption_id)
                .Index(t => t.Bet_Bet_id)
                .Index(t => t.DeliveryOption_DeliveryOption_id);
            
            CreateTable(
                "dbo.DeliveryOptions",
                c => new
                    {
                        DeliveryOption_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Source = c.String(),
                        Destination = c.String(),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DeliveryOption_id);
            
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        Delivery_id = c.Int(nullable: false, identity: true),
                        ShipmentLastDate = c.DateTime(nullable: false),
                        Track = c.String(),
                        DeliveryOption_DeliveryOption_id = c.Int(),
                    })
                .PrimaryKey(t => t.Delivery_id)
                .ForeignKey("dbo.DeliveryOptions", t => t.DeliveryOption_DeliveryOption_id)
                .Index(t => t.DeliveryOption_DeliveryOption_id);
            
            CreateTable(
                "dbo.DeliveryStatus",
                c => new
                    {
                        DeliveryStatus_id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Delivery_Delivery_id = c.Int(),
                    })
                .PrimaryKey(t => t.DeliveryStatus_id)
                .ForeignKey("dbo.Deliveries", t => t.Delivery_Delivery_id)
                .Index(t => t.Delivery_Delivery_id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Customer_id = c.Int(nullable: false, identity: true),
                        Figure_Figure_id = c.Int(),
                    })
                .PrimaryKey(t => t.Customer_id)
                .ForeignKey("dbo.Figures", t => t.Figure_Figure_id)
                .Index(t => t.Figure_Figure_id);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Favorite_id = c.Int(nullable: false, identity: true),
                        Customer_Customer_id = c.Int(),
                        Lot_Lot_id = c.Int(),
                    })
                .PrimaryKey(t => t.Favorite_id)
                .ForeignKey("dbo.Customers", t => t.Customer_Customer_id)
                .ForeignKey("dbo.Lots", t => t.Lot_Lot_id)
                .Index(t => t.Customer_Customer_id)
                .Index(t => t.Lot_Lot_id);
            
            CreateTable(
                "dbo.Lots",
                c => new
                    {
                        Lot_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        Start_bet = c.Double(nullable: false),
                        Min_bet = c.Double(nullable: false),
                        Auction_start = c.DateTime(nullable: false),
                        Auction_end = c.DateTime(nullable: false),
                        Status = c.String(),
                        Seller_Seller_id = c.Int(),
                    })
                .PrimaryKey(t => t.Lot_id)
                .ForeignKey("dbo.Sellers", t => t.Seller_Seller_id)
                .Index(t => t.Seller_Seller_id);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        Seller_id = c.Int(nullable: false, identity: true),
                        Rating = c.Double(nullable: false),
                        Figure_Figure_id = c.Int(),
                    })
                .PrimaryKey(t => t.Seller_id)
                .ForeignKey("dbo.Figures", t => t.Figure_Figure_id)
                .Index(t => t.Figure_Figure_id);
            
            CreateTable(
                "dbo.Figures",
                c => new
                    {
                        Figure_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Patronymic = c.String(),
                        Location = c.String(),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Figure_id)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Reg_date = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Disputes",
                c => new
                    {
                        Dispute_id = c.Int(nullable: false, identity: true),
                        Reason = c.String(),
                        Status = c.String(),
                        Trade_Trade_id = c.Int(),
                    })
                .PrimaryKey(t => t.Dispute_id)
                .ForeignKey("dbo.Trades", t => t.Trade_Trade_id)
                .Index(t => t.Trade_Trade_id);
            
            CreateTable(
                "dbo.Trades",
                c => new
                    {
                        Trade_id = c.Int(nullable: false, identity: true),
                        Close_date = c.DateTime(nullable: false),
                        Status = c.String(),
                        Check_Check_id = c.Int(),
                        Delivery_Delivery_id = c.Int(),
                    })
                .PrimaryKey(t => t.Trade_id)
                .ForeignKey("dbo.Checks", t => t.Check_Check_id)
                .ForeignKey("dbo.Deliveries", t => t.Delivery_Delivery_id)
                .Index(t => t.Check_Check_id)
                .Index(t => t.Delivery_Delivery_id);
            
            CreateTable(
                "dbo.Refunds",
                c => new
                    {
                        Refund_id = c.Int(nullable: false, identity: true),
                        Dispute_Dispute_id = c.Int(),
                    })
                .PrimaryKey(t => t.Refund_id)
                .ForeignKey("dbo.Disputes", t => t.Dispute_Dispute_id)
                .Index(t => t.Dispute_Dispute_id);
            
            CreateTable(
                "dbo.Returns",
                c => new
                    {
                        Return_id = c.Int(nullable: false, identity: true),
                        Delivery_Delivery_id = c.Int(),
                        Dispute_Dispute_id = c.Int(),
                    })
                .PrimaryKey(t => t.Return_id)
                .ForeignKey("dbo.Deliveries", t => t.Delivery_Delivery_id)
                .ForeignKey("dbo.Disputes", t => t.Dispute_Dispute_id)
                .Index(t => t.Delivery_Delivery_id)
                .Index(t => t.Dispute_Dispute_id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Setting_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Setting_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Returns", "Dispute_Dispute_id", "dbo.Disputes");
            DropForeignKey("dbo.Returns", "Delivery_Delivery_id", "dbo.Deliveries");
            DropForeignKey("dbo.Refunds", "Dispute_Dispute_id", "dbo.Disputes");
            DropForeignKey("dbo.Disputes", "Trade_Trade_id", "dbo.Trades");
            DropForeignKey("dbo.Trades", "Delivery_Delivery_id", "dbo.Deliveries");
            DropForeignKey("dbo.Trades", "Check_Check_id", "dbo.Checks");
            DropForeignKey("dbo.Customers", "Figure_Figure_id", "dbo.Figures");
            DropForeignKey("dbo.Lots", "Seller_Seller_id", "dbo.Sellers");
            DropForeignKey("dbo.Sellers", "Figure_Figure_id", "dbo.Figures");
            DropForeignKey("dbo.Figures", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Favorites", "Lot_Lot_id", "dbo.Lots");
            DropForeignKey("dbo.Bets", "Lot_Lot_id", "dbo.Lots");
            DropForeignKey("dbo.Favorites", "Customer_Customer_id", "dbo.Customers");
            DropForeignKey("dbo.Bets", "Customer_Customer_id", "dbo.Customers");
            DropForeignKey("dbo.Deliveries", "DeliveryOption_DeliveryOption_id", "dbo.DeliveryOptions");
            DropForeignKey("dbo.DeliveryStatus", "Delivery_Delivery_id", "dbo.Deliveries");
            DropForeignKey("dbo.Checks", "DeliveryOption_DeliveryOption_id", "dbo.DeliveryOptions");
            DropForeignKey("dbo.Checks", "Bet_Bet_id", "dbo.Bets");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Returns", new[] { "Dispute_Dispute_id" });
            DropIndex("dbo.Returns", new[] { "Delivery_Delivery_id" });
            DropIndex("dbo.Refunds", new[] { "Dispute_Dispute_id" });
            DropIndex("dbo.Trades", new[] { "Delivery_Delivery_id" });
            DropIndex("dbo.Trades", new[] { "Check_Check_id" });
            DropIndex("dbo.Disputes", new[] { "Trade_Trade_id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Figures", new[] { "AppUser_Id" });
            DropIndex("dbo.Sellers", new[] { "Figure_Figure_id" });
            DropIndex("dbo.Lots", new[] { "Seller_Seller_id" });
            DropIndex("dbo.Favorites", new[] { "Lot_Lot_id" });
            DropIndex("dbo.Favorites", new[] { "Customer_Customer_id" });
            DropIndex("dbo.Customers", new[] { "Figure_Figure_id" });
            DropIndex("dbo.DeliveryStatus", new[] { "Delivery_Delivery_id" });
            DropIndex("dbo.Deliveries", new[] { "DeliveryOption_DeliveryOption_id" });
            DropIndex("dbo.Checks", new[] { "DeliveryOption_DeliveryOption_id" });
            DropIndex("dbo.Checks", new[] { "Bet_Bet_id" });
            DropIndex("dbo.Bets", new[] { "Lot_Lot_id" });
            DropIndex("dbo.Bets", new[] { "Customer_Customer_id" });
            DropTable("dbo.Settings");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Returns");
            DropTable("dbo.Refunds");
            DropTable("dbo.Trades");
            DropTable("dbo.Disputes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Figures");
            DropTable("dbo.Sellers");
            DropTable("dbo.Lots");
            DropTable("dbo.Favorites");
            DropTable("dbo.Customers");
            DropTable("dbo.DeliveryStatus");
            DropTable("dbo.Deliveries");
            DropTable("dbo.DeliveryOptions");
            DropTable("dbo.Checks");
            DropTable("dbo.Bets");
        }
    }
}
