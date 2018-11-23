namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration15 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DeliveringConditions", "Shipment_Shipment_id", "dbo.Shipments");
            DropForeignKey("dbo.Shipments", "DeliveringCondition_DeliveringCondition_id", "dbo.DeliveringConditions");
            DropForeignKey("dbo.DeliveringConditions", "Shipment_Shipment_id1", "dbo.Shipments");
            DropForeignKey("dbo.Shipments", "DeliveryCondition_DeliveringCondition_id", "dbo.DeliveringConditions");
            DropIndex("dbo.Shipments", new[] { "DeliveringCondition_DeliveringCondition_id" });
            DropIndex("dbo.Shipments", new[] { "DeliveryCondition_DeliveringCondition_id" });
            DropIndex("dbo.DeliveringConditions", new[] { "Shipment_Shipment_id" });
            DropIndex("dbo.DeliveringConditions", new[] { "Shipment_Shipment_id1" });
            CreateTable(
                "dbo.ShipmentDeliveringConditions",
                c => new
                    {
                        ShipmentDeliveringCondition_id = c.Int(nullable: false, identity: true),
                        DeliveryCondition_DeliveringCondition_id = c.Int(),
                        Shipment_Shipment_id = c.Int(),
                    })
                .PrimaryKey(t => t.ShipmentDeliveringCondition_id)
                .ForeignKey("dbo.DeliveringConditions", t => t.DeliveryCondition_DeliveringCondition_id)
                .ForeignKey("dbo.Shipments", t => t.Shipment_Shipment_id)
                .Index(t => t.DeliveryCondition_DeliveringCondition_id)
                .Index(t => t.Shipment_Shipment_id);
            
            DropColumn("dbo.Shipments", "DeliveringCondition_DeliveringCondition_id");
            DropColumn("dbo.Shipments", "DeliveryCondition_DeliveringCondition_id");
            DropColumn("dbo.DeliveringConditions", "Shipment_Shipment_id");
            DropColumn("dbo.DeliveringConditions", "Shipment_Shipment_id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeliveringConditions", "Shipment_Shipment_id1", c => c.Int());
            AddColumn("dbo.DeliveringConditions", "Shipment_Shipment_id", c => c.Int());
            AddColumn("dbo.Shipments", "DeliveryCondition_DeliveringCondition_id", c => c.Int());
            AddColumn("dbo.Shipments", "DeliveringCondition_DeliveringCondition_id", c => c.Int());
            DropForeignKey("dbo.ShipmentDeliveringConditions", "Shipment_Shipment_id", "dbo.Shipments");
            DropForeignKey("dbo.ShipmentDeliveringConditions", "DeliveryCondition_DeliveringCondition_id", "dbo.DeliveringConditions");
            DropIndex("dbo.ShipmentDeliveringConditions", new[] { "Shipment_Shipment_id" });
            DropIndex("dbo.ShipmentDeliveringConditions", new[] { "DeliveryCondition_DeliveringCondition_id" });
            DropTable("dbo.ShipmentDeliveringConditions");
            CreateIndex("dbo.DeliveringConditions", "Shipment_Shipment_id1");
            CreateIndex("dbo.DeliveringConditions", "Shipment_Shipment_id");
            CreateIndex("dbo.Shipments", "DeliveryCondition_DeliveringCondition_id");
            CreateIndex("dbo.Shipments", "DeliveringCondition_DeliveringCondition_id");
            AddForeignKey("dbo.Shipments", "DeliveryCondition_DeliveringCondition_id", "dbo.DeliveringConditions", "DeliveringCondition_id");
            AddForeignKey("dbo.DeliveringConditions", "Shipment_Shipment_id1", "dbo.Shipments", "Shipment_id");
            AddForeignKey("dbo.Shipments", "DeliveringCondition_DeliveringCondition_id", "dbo.DeliveringConditions", "DeliveringCondition_id");
            AddForeignKey("dbo.DeliveringConditions", "Shipment_Shipment_id", "dbo.Shipments", "Shipment_id");
        }
    }
}
