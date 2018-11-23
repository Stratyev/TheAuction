namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Sets", newName: "Settings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Settings", newName: "Sets");
        }
    }
}
