namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LotGroups", "LotRangeStart", c => c.Int(nullable: false));
            AddColumn("dbo.LotGroups", "LotRangeEnd", c => c.Int(nullable: false));
            DropColumn("dbo.LotGroups", "LotRange");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LotGroups", "LotRange", c => c.Double(nullable: false));
            DropColumn("dbo.LotGroups", "LotRangeEnd");
            DropColumn("dbo.LotGroups", "LotRangeStart");
        }
    }
}
