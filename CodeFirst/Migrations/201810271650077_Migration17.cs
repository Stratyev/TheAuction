namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LotGroups",
                c => new
                    {
                        LotGroup_id = c.Int(nullable: false, identity: true),
                        LotRange = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.LotGroup_id);
            
            AddColumn("dbo.Lots", "LotGroup_LotGroup_id", c => c.Int());
            CreateIndex("dbo.Lots", "LotGroup_LotGroup_id");
            AddForeignKey("dbo.Lots", "LotGroup_LotGroup_id", "dbo.LotGroups", "LotGroup_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "LotGroup_LotGroup_id", "dbo.LotGroups");
            DropIndex("dbo.Lots", new[] { "LotGroup_LotGroup_id" });
            DropColumn("dbo.Lots", "LotGroup_LotGroup_id");
            DropTable("dbo.LotGroups");
        }
    }
}
