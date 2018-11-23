namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveringConditions",
                c => new
                    {
                        DeliveringCondition_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Shipment_Shipment_id = c.Int(),
                        Shipment_Shipment_id1 = c.Int(),
                    })
                .PrimaryKey(t => t.DeliveringCondition_id)
                .ForeignKey("dbo.Shipments", t => t.Shipment_Shipment_id)
                .ForeignKey("dbo.Shipments", t => t.Shipment_Shipment_id1)
                .Index(t => t.Shipment_Shipment_id)
                .Index(t => t.Shipment_Shipment_id1);
            
            AddColumn("dbo.Shipments", "DeliveringCondition_DeliveringCondition_id", c => c.Int());
            AddColumn("dbo.Shipments", "DeliveryCondition_DeliveringCondition_id", c => c.Int());
            CreateIndex("dbo.Shipments", "DeliveringCondition_DeliveringCondition_id");
            CreateIndex("dbo.Shipments", "DeliveryCondition_DeliveringCondition_id");
            AddForeignKey("dbo.Shipments", "DeliveringCondition_DeliveringCondition_id", "dbo.DeliveringConditions", "DeliveringCondition_id");
            AddForeignKey("dbo.Shipments", "DeliveryCondition_DeliveringCondition_id", "dbo.DeliveringConditions", "DeliveringCondition_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shipments", "DeliveryCondition_DeliveringCondition_id", "dbo.DeliveringConditions");
            DropForeignKey("dbo.DeliveringConditions", "Shipment_Shipment_id1", "dbo.Shipments");
            DropForeignKey("dbo.Shipments", "DeliveringCondition_DeliveringCondition_id", "dbo.DeliveringConditions");
            DropForeignKey("dbo.DeliveringConditions", "Shipment_Shipment_id", "dbo.Shipments");
            DropIndex("dbo.DeliveringConditions", new[] { "Shipment_Shipment_id1" });
            DropIndex("dbo.DeliveringConditions", new[] { "Shipment_Shipment_id" });
            DropIndex("dbo.Shipments", new[] { "DeliveryCondition_DeliveringCondition_id" });
            DropIndex("dbo.Shipments", new[] { "DeliveringCondition_DeliveringCondition_id" });
            DropColumn("dbo.Shipments", "DeliveryCondition_DeliveringCondition_id");
            DropColumn("dbo.Shipments", "DeliveringCondition_DeliveringCondition_id");
            DropTable("dbo.DeliveringConditions");
        }
    }
}
