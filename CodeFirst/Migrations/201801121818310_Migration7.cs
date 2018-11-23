namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Location_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Location_id);
            
            AddColumn("dbo.Lots", "Location_Location_id", c => c.Int());
            AddColumn("dbo.Figures", "Location_Location_id", c => c.Int());
            AddColumn("dbo.ShipmentOptions", "Destination_Location_id", c => c.Int());
            AddColumn("dbo.ShipmentOptions", "Source_Location_id", c => c.Int());
            CreateIndex("dbo.Lots", "Location_Location_id");
            CreateIndex("dbo.Figures", "Location_Location_id");
            CreateIndex("dbo.ShipmentOptions", "Destination_Location_id");
            CreateIndex("dbo.ShipmentOptions", "Source_Location_id");
            AddForeignKey("dbo.Figures", "Location_Location_id", "dbo.Locations", "Location_id");
            AddForeignKey("dbo.Lots", "Location_Location_id", "dbo.Locations", "Location_id");
            AddForeignKey("dbo.ShipmentOptions", "Destination_Location_id", "dbo.Locations", "Location_id");
            AddForeignKey("dbo.ShipmentOptions", "Source_Location_id", "dbo.Locations", "Location_id");
            DropColumn("dbo.Figures", "Location");
            DropColumn("dbo.ShipmentOptions", "Source");
            DropColumn("dbo.ShipmentOptions", "Destination");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShipmentOptions", "Destination", c => c.String());
            AddColumn("dbo.ShipmentOptions", "Source", c => c.String());
            AddColumn("dbo.Figures", "Location", c => c.String());
            DropForeignKey("dbo.ShipmentOptions", "Source_Location_id", "dbo.Locations");
            DropForeignKey("dbo.ShipmentOptions", "Destination_Location_id", "dbo.Locations");
            DropForeignKey("dbo.Lots", "Location_Location_id", "dbo.Locations");
            DropForeignKey("dbo.Figures", "Location_Location_id", "dbo.Locations");
            DropIndex("dbo.ShipmentOptions", new[] { "Source_Location_id" });
            DropIndex("dbo.ShipmentOptions", new[] { "Destination_Location_id" });
            DropIndex("dbo.Figures", new[] { "Location_Location_id" });
            DropIndex("dbo.Lots", new[] { "Location_Location_id" });
            DropColumn("dbo.ShipmentOptions", "Source_Location_id");
            DropColumn("dbo.ShipmentOptions", "Destination_Location_id");
            DropColumn("dbo.Figures", "Location_Location_id");
            DropColumn("dbo.Lots", "Location_Location_id");
            DropTable("dbo.Locations");
        }
    }
}
