namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Lots", name: "Winner_Figure_id", newName: "Winner_Customer_id");
            RenameIndex(table: "dbo.Lots", name: "IX_Winner_Figure_id", newName: "IX_Winner_Customer_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Lots", name: "IX_Winner_Customer_id", newName: "IX_Winner_Figure_id");
            RenameColumn(table: "dbo.Lots", name: "Winner_Customer_id", newName: "Winner_Figure_id");
        }
    }
}
