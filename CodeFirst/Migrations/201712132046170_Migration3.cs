namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "Winner_Figure_id", c => c.Int());
            CreateIndex("dbo.Lots", "Winner_Figure_id");
            AddForeignKey("dbo.Lots", "Winner_Figure_id", "dbo.Figures", "Figure_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "Winner_Figure_id", "dbo.Figures");
            DropIndex("dbo.Lots", new[] { "Winner_Figure_id" });
            DropColumn("dbo.Lots", "Winner_Figure_id");
        }
    }
}
