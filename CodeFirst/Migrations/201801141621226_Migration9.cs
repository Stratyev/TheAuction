namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Category_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Category_id);
            
            AddColumn("dbo.Lots", "Category_Category_id", c => c.Int());
            CreateIndex("dbo.Lots", "Category_Category_id");
            AddForeignKey("dbo.Lots", "Category_Category_id", "dbo.Categories", "Category_id");
            DropColumn("dbo.Lots", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "Category", c => c.String());
            DropForeignKey("dbo.Lots", "Category_Category_id", "dbo.Categories");
            DropIndex("dbo.Lots", new[] { "Category_Category_id" });
            DropColumn("dbo.Lots", "Category_Category_id");
            DropTable("dbo.Categories");
        }
    }
}
