namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lots", "Category");
        }
    }
}
