namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Figures", "PictureUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Figures", "PictureUrl");
        }
    }
}
