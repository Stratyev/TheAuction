namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Status_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Status_id);
            
            AddColumn("dbo.Checks", "Status_Status_id", c => c.Int());
            AddColumn("dbo.Lots", "Status_Status_id", c => c.Int());
            AddColumn("dbo.Shipments", "Status_Status_id", c => c.Int());
            CreateIndex("dbo.Checks", "Status_Status_id");
            CreateIndex("dbo.Lots", "Status_Status_id");
            CreateIndex("dbo.Shipments", "Status_Status_id");
            AddForeignKey("dbo.Checks", "Status_Status_id", "dbo.Status", "Status_id");
            AddForeignKey("dbo.Lots", "Status_Status_id", "dbo.Status", "Status_id");
            AddForeignKey("dbo.Shipments", "Status_Status_id", "dbo.Status", "Status_id");
            DropColumn("dbo.Checks", "Status");
            DropColumn("dbo.Lots", "Status");
            DropColumn("dbo.Shipments", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shipments", "Status", c => c.String());
            AddColumn("dbo.Lots", "Status", c => c.String());
            AddColumn("dbo.Checks", "Status", c => c.String());
            DropForeignKey("dbo.Shipments", "Status_Status_id", "dbo.Status");
            DropForeignKey("dbo.Lots", "Status_Status_id", "dbo.Status");
            DropForeignKey("dbo.Checks", "Status_Status_id", "dbo.Status");
            DropIndex("dbo.Shipments", new[] { "Status_Status_id" });
            DropIndex("dbo.Lots", new[] { "Status_Status_id" });
            DropIndex("dbo.Checks", new[] { "Status_Status_id" });
            DropColumn("dbo.Shipments", "Status_Status_id");
            DropColumn("dbo.Lots", "Status_Status_id");
            DropColumn("dbo.Checks", "Status_Status_id");
            DropTable("dbo.Status");
        }
    }
}
