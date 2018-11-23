namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShipmentStatus", "Shipment_Shipment_id", "dbo.Shipments");
            DropForeignKey("dbo.Shipments", "ShipmentOption_ShipmentOption_id", "dbo.ShipmentOptions");
            DropForeignKey("dbo.Trades", "Check_Check_id", "dbo.Checks");
            DropForeignKey("dbo.Disputes", "Trade_Trade_id", "dbo.Trades");
            DropForeignKey("dbo.Trades", "Shipment_Shipment_id", "dbo.Shipments");
            DropForeignKey("dbo.Refunds", "Dispute_Dispute_id", "dbo.Disputes");
            DropForeignKey("dbo.Returns", "Dispute_Dispute_id", "dbo.Disputes");
            DropForeignKey("dbo.Returns", "Shipment_Shipment_id", "dbo.Shipments");
            DropIndex("dbo.Shipments", new[] { "ShipmentOption_ShipmentOption_id" });
            DropIndex("dbo.ShipmentStatus", new[] { "Shipment_Shipment_id" });
            DropIndex("dbo.Disputes", new[] { "Trade_Trade_id" });
            DropIndex("dbo.Trades", new[] { "Check_Check_id" });
            DropIndex("dbo.Trades", new[] { "Shipment_Shipment_id" });
            DropIndex("dbo.Refunds", new[] { "Dispute_Dispute_id" });
            DropIndex("dbo.Returns", new[] { "Dispute_Dispute_id" });
            DropIndex("dbo.Returns", new[] { "Shipment_Shipment_id" });
            AddColumn("dbo.Checks", "ShipmentLastDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Checks", "Lot_Lot_id", c => c.Int());
            AddColumn("dbo.Shipments", "Status", c => c.String());
            AddColumn("dbo.Shipments", "Check_Check_id", c => c.Int());
            AddColumn("dbo.Lots", "Bets_count", c => c.Int(nullable: false));
            CreateIndex("dbo.Checks", "Lot_Lot_id");
            CreateIndex("dbo.Shipments", "Check_Check_id");
            AddForeignKey("dbo.Checks", "Lot_Lot_id", "dbo.Lots", "Lot_id");
            AddForeignKey("dbo.Shipments", "Check_Check_id", "dbo.Checks", "Check_id");
            DropColumn("dbo.Shipments", "ShipmentLastDate");
            DropColumn("dbo.Shipments", "ShipmentOption_ShipmentOption_id");
            DropTable("dbo.ShipmentStatus");
            DropTable("dbo.Disputes");
            DropTable("dbo.Trades");
            DropTable("dbo.Refunds");
            DropTable("dbo.Returns");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Returns",
                c => new
                    {
                        Return_id = c.Int(nullable: false, identity: true),
                        Dispute_Dispute_id = c.Int(),
                        Shipment_Shipment_id = c.Int(),
                    })
                .PrimaryKey(t => t.Return_id);
            
            CreateTable(
                "dbo.Refunds",
                c => new
                    {
                        Refund_id = c.Int(nullable: false, identity: true),
                        Dispute_Dispute_id = c.Int(),
                    })
                .PrimaryKey(t => t.Refund_id);
            
            CreateTable(
                "dbo.Trades",
                c => new
                    {
                        Trade_id = c.Int(nullable: false, identity: true),
                        Close_date = c.DateTime(nullable: false),
                        Status = c.String(),
                        Check_Check_id = c.Int(),
                        Shipment_Shipment_id = c.Int(),
                    })
                .PrimaryKey(t => t.Trade_id);
            
            CreateTable(
                "dbo.Disputes",
                c => new
                    {
                        Dispute_id = c.Int(nullable: false, identity: true),
                        Reason = c.String(),
                        Status = c.String(),
                        Trade_Trade_id = c.Int(),
                    })
                .PrimaryKey(t => t.Dispute_id);
            
            CreateTable(
                "dbo.ShipmentStatus",
                c => new
                    {
                        ShipmentStatus_id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Shipment_Shipment_id = c.Int(),
                    })
                .PrimaryKey(t => t.ShipmentStatus_id);
            
            AddColumn("dbo.Shipments", "ShipmentOption_ShipmentOption_id", c => c.Int());
            AddColumn("dbo.Shipments", "ShipmentLastDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Shipments", "Check_Check_id", "dbo.Checks");
            DropForeignKey("dbo.Checks", "Lot_Lot_id", "dbo.Lots");
            DropIndex("dbo.Shipments", new[] { "Check_Check_id" });
            DropIndex("dbo.Checks", new[] { "Lot_Lot_id" });
            DropColumn("dbo.Lots", "Bets_count");
            DropColumn("dbo.Shipments", "Check_Check_id");
            DropColumn("dbo.Shipments", "Status");
            DropColumn("dbo.Checks", "Lot_Lot_id");
            DropColumn("dbo.Checks", "ShipmentLastDate");
            CreateIndex("dbo.Returns", "Shipment_Shipment_id");
            CreateIndex("dbo.Returns", "Dispute_Dispute_id");
            CreateIndex("dbo.Refunds", "Dispute_Dispute_id");
            CreateIndex("dbo.Trades", "Shipment_Shipment_id");
            CreateIndex("dbo.Trades", "Check_Check_id");
            CreateIndex("dbo.Disputes", "Trade_Trade_id");
            CreateIndex("dbo.ShipmentStatus", "Shipment_Shipment_id");
            CreateIndex("dbo.Shipments", "ShipmentOption_ShipmentOption_id");
            AddForeignKey("dbo.Returns", "Shipment_Shipment_id", "dbo.Shipments", "Shipment_id");
            AddForeignKey("dbo.Returns", "Dispute_Dispute_id", "dbo.Disputes", "Dispute_id");
            AddForeignKey("dbo.Refunds", "Dispute_Dispute_id", "dbo.Disputes", "Dispute_id");
            AddForeignKey("dbo.Trades", "Shipment_Shipment_id", "dbo.Shipments", "Shipment_id");
            AddForeignKey("dbo.Disputes", "Trade_Trade_id", "dbo.Trades", "Trade_id");
            AddForeignKey("dbo.Trades", "Check_Check_id", "dbo.Checks", "Check_id");
            AddForeignKey("dbo.Shipments", "ShipmentOption_ShipmentOption_id", "dbo.ShipmentOptions", "ShipmentOption_id");
            AddForeignKey("dbo.ShipmentStatus", "Shipment_Shipment_id", "dbo.Shipments", "Shipment_id");
        }
    }
}
