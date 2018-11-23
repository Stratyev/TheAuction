namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Settings", newName: "Sets");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Sets", newName: "Settings");
        }
    }
}
