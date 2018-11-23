namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Checks", "Status_Status_id", "dbo.Status");
            DropForeignKey("dbo.Lots", "Status_Status_id", "dbo.Status");
            DropForeignKey("dbo.Shipments", "Status_Status_id", "dbo.Status");
            DropIndex("dbo.Checks", new[] { "Status_Status_id" });
            DropIndex("dbo.Lots", new[] { "Status_Status_id" });
            DropIndex("dbo.Shipments", new[] { "Status_Status_id" });
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        Condition_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Condition_id);
            
            AddColumn("dbo.Checks", "Condition_Condition_id", c => c.Int());
            AddColumn("dbo.Lots", "Condition_Condition_id", c => c.Int());
            AddColumn("dbo.Shipments", "Condition_Condition_id", c => c.Int());
            CreateIndex("dbo.Checks", "Condition_Condition_id");
            CreateIndex("dbo.Lots", "Condition_Condition_id");
            CreateIndex("dbo.Shipments", "Condition_Condition_id");
            AddForeignKey("dbo.Checks", "Condition_Condition_id", "dbo.Conditions", "Condition_id");
            AddForeignKey("dbo.Lots", "Condition_Condition_id", "dbo.Conditions", "Condition_id");
            AddForeignKey("dbo.Shipments", "Condition_Condition_id", "dbo.Conditions", "Condition_id");
            DropColumn("dbo.Checks", "Status_Status_id");
            DropColumn("dbo.Lots", "Status_Status_id");
            DropColumn("dbo.Shipments", "Status_Status_id");
            DropTable("dbo.Status");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Status_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Status_id);
            
            AddColumn("dbo.Shipments", "Status_Status_id", c => c.Int());
            AddColumn("dbo.Lots", "Status_Status_id", c => c.Int());
            AddColumn("dbo.Checks", "Status_Status_id", c => c.Int());
            DropForeignKey("dbo.Shipments", "Condition_Condition_id", "dbo.Conditions");
            DropForeignKey("dbo.Lots", "Condition_Condition_id", "dbo.Conditions");
            DropForeignKey("dbo.Checks", "Condition_Condition_id", "dbo.Conditions");
            DropIndex("dbo.Shipments", new[] { "Condition_Condition_id" });
            DropIndex("dbo.Lots", new[] { "Condition_Condition_id" });
            DropIndex("dbo.Checks", new[] { "Condition_Condition_id" });
            DropColumn("dbo.Shipments", "Condition_Condition_id");
            DropColumn("dbo.Lots", "Condition_Condition_id");
            DropColumn("dbo.Checks", "Condition_Condition_id");
            DropTable("dbo.Conditions");
            CreateIndex("dbo.Shipments", "Status_Status_id");
            CreateIndex("dbo.Lots", "Status_Status_id");
            CreateIndex("dbo.Checks", "Status_Status_id");
            AddForeignKey("dbo.Shipments", "Status_Status_id", "dbo.Status", "Status_id");
            AddForeignKey("dbo.Lots", "Status_Status_id", "dbo.Status", "Status_id");
            AddForeignKey("dbo.Checks", "Status_Status_id", "dbo.Status", "Status_id");
        }
    }
}
