namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Checks", "DeliveryOption_DeliveryOption_id", "dbo.DeliveryOptions");
            DropForeignKey("dbo.DeliveryStatus", "Delivery_Delivery_id", "dbo.Deliveries");
            DropForeignKey("dbo.Deliveries", "DeliveryOption_DeliveryOption_id", "dbo.DeliveryOptions");
            DropForeignKey("dbo.Trades", "Delivery_Delivery_id", "dbo.Deliveries");
            DropForeignKey("dbo.Returns", "Delivery_Delivery_id", "dbo.Deliveries");
            DropIndex("dbo.Checks", new[] { "DeliveryOption_DeliveryOption_id" });
            DropIndex("dbo.Deliveries", new[] { "DeliveryOption_DeliveryOption_id" });
            DropIndex("dbo.DeliveryStatus", new[] { "Delivery_Delivery_id" });
            DropIndex("dbo.Trades", new[] { "Delivery_Delivery_id" });
            DropIndex("dbo.Returns", new[] { "Delivery_Delivery_id" });
            CreateTable(
                "dbo.ShipmentOptions",
                c => new
                    {
                        ShipmentOption_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Source = c.String(),
                        Destination = c.String(),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ShipmentOption_id);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        Shipment_id = c.Int(nullable: false, identity: true),
                        ShipmentLastDate = c.DateTime(nullable: false),
                        Track = c.String(),
                        ShipmentOption_ShipmentOption_id = c.Int(),
                    })
                .PrimaryKey(t => t.Shipment_id)
                .ForeignKey("dbo.ShipmentOptions", t => t.ShipmentOption_ShipmentOption_id)
                .Index(t => t.ShipmentOption_ShipmentOption_id);
            
            CreateTable(
                "dbo.ShipmentStatus",
                c => new
                    {
                        ShipmentStatus_id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Shipment_Shipment_id = c.Int(),
                    })
                .PrimaryKey(t => t.ShipmentStatus_id)
                .ForeignKey("dbo.Shipments", t => t.Shipment_Shipment_id)
                .Index(t => t.Shipment_Shipment_id);
            
            AddColumn("dbo.Checks", "ShipmentOption_ShipmentOption_id", c => c.Int());
            AddColumn("dbo.Trades", "Shipment_Shipment_id", c => c.Int());
            AddColumn("dbo.Returns", "Shipment_Shipment_id", c => c.Int());
            CreateIndex("dbo.Checks", "ShipmentOption_ShipmentOption_id");
            CreateIndex("dbo.Trades", "Shipment_Shipment_id");
            CreateIndex("dbo.Returns", "Shipment_Shipment_id");
            AddForeignKey("dbo.Checks", "ShipmentOption_ShipmentOption_id", "dbo.ShipmentOptions", "ShipmentOption_id");
            AddForeignKey("dbo.Trades", "Shipment_Shipment_id", "dbo.Shipments", "Shipment_id");
            AddForeignKey("dbo.Returns", "Shipment_Shipment_id", "dbo.Shipments", "Shipment_id");
            DropColumn("dbo.Checks", "DeliveryOption_DeliveryOption_id");
            DropColumn("dbo.Trades", "Delivery_Delivery_id");
            DropColumn("dbo.Returns", "Delivery_Delivery_id");
            DropTable("dbo.DeliveryOptions");
            DropTable("dbo.Deliveries");
            DropTable("dbo.DeliveryStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DeliveryStatus",
                c => new
                    {
                        DeliveryStatus_id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Delivery_Delivery_id = c.Int(),
                    })
                .PrimaryKey(t => t.DeliveryStatus_id);
            
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        Delivery_id = c.Int(nullable: false, identity: true),
                        ShipmentLastDate = c.DateTime(nullable: false),
                        Track = c.String(),
                        DeliveryOption_DeliveryOption_id = c.Int(),
                    })
                .PrimaryKey(t => t.Delivery_id);
            
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
            
            AddColumn("dbo.Returns", "Delivery_Delivery_id", c => c.Int());
            AddColumn("dbo.Trades", "Delivery_Delivery_id", c => c.Int());
            AddColumn("dbo.Checks", "DeliveryOption_DeliveryOption_id", c => c.Int());
            DropForeignKey("dbo.Returns", "Shipment_Shipment_id", "dbo.Shipments");
            DropForeignKey("dbo.Trades", "Shipment_Shipment_id", "dbo.Shipments");
            DropForeignKey("dbo.Shipments", "ShipmentOption_ShipmentOption_id", "dbo.ShipmentOptions");
            DropForeignKey("dbo.ShipmentStatus", "Shipment_Shipment_id", "dbo.Shipments");
            DropForeignKey("dbo.Checks", "ShipmentOption_ShipmentOption_id", "dbo.ShipmentOptions");
            DropIndex("dbo.Returns", new[] { "Shipment_Shipment_id" });
            DropIndex("dbo.Trades", new[] { "Shipment_Shipment_id" });
            DropIndex("dbo.ShipmentStatus", new[] { "Shipment_Shipment_id" });
            DropIndex("dbo.Shipments", new[] { "ShipmentOption_ShipmentOption_id" });
            DropIndex("dbo.Checks", new[] { "ShipmentOption_ShipmentOption_id" });
            DropColumn("dbo.Returns", "Shipment_Shipment_id");
            DropColumn("dbo.Trades", "Shipment_Shipment_id");
            DropColumn("dbo.Checks", "ShipmentOption_ShipmentOption_id");
            DropTable("dbo.ShipmentStatus");
            DropTable("dbo.Shipments");
            DropTable("dbo.ShipmentOptions");
            CreateIndex("dbo.Returns", "Delivery_Delivery_id");
            CreateIndex("dbo.Trades", "Delivery_Delivery_id");
            CreateIndex("dbo.DeliveryStatus", "Delivery_Delivery_id");
            CreateIndex("dbo.Deliveries", "DeliveryOption_DeliveryOption_id");
            CreateIndex("dbo.Checks", "DeliveryOption_DeliveryOption_id");
            AddForeignKey("dbo.Returns", "Delivery_Delivery_id", "dbo.Deliveries", "Delivery_id");
            AddForeignKey("dbo.Trades", "Delivery_Delivery_id", "dbo.Deliveries", "Delivery_id");
            AddForeignKey("dbo.Deliveries", "DeliveryOption_DeliveryOption_id", "dbo.DeliveryOptions", "DeliveryOption_id");
            AddForeignKey("dbo.DeliveryStatus", "Delivery_Delivery_id", "dbo.Deliveries", "Delivery_id");
            AddForeignKey("dbo.Checks", "DeliveryOption_DeliveryOption_id", "dbo.DeliveryOptions", "DeliveryOption_id");
        }
    }
}
