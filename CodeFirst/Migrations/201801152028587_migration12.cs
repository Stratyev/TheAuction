namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LotPictures",
                c => new
                    {
                        LotPicture_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.LotPicture_id);
            
            CreateTable(
                "dbo.FigurePictures",
                c => new
                    {
                        UserPicture_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.UserPicture_id);
            
            CreateTable(
                "dbo.LotPictureLots",
                c => new
                    {
                        LotPicture_LotPicture_id = c.Int(nullable: false),
                        Lot_Lot_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LotPicture_LotPicture_id, t.Lot_Lot_id })
                .ForeignKey("dbo.LotPictures", t => t.LotPicture_LotPicture_id, cascadeDelete: true)
                .ForeignKey("dbo.Lots", t => t.Lot_Lot_id, cascadeDelete: true)
                .Index(t => t.LotPicture_LotPicture_id)
                .Index(t => t.Lot_Lot_id);
            
            AddColumn("dbo.Figures", "FigurePicture_UserPicture_id", c => c.Int());
            CreateIndex("dbo.Figures", "FigurePicture_UserPicture_id");
            AddForeignKey("dbo.Figures", "FigurePicture_UserPicture_id", "dbo.FigurePictures", "UserPicture_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Figures", "FigurePicture_UserPicture_id", "dbo.FigurePictures");
            DropForeignKey("dbo.LotPictureLots", "Lot_Lot_id", "dbo.Lots");
            DropForeignKey("dbo.LotPictureLots", "LotPicture_LotPicture_id", "dbo.LotPictures");
            DropIndex("dbo.LotPictureLots", new[] { "Lot_Lot_id" });
            DropIndex("dbo.LotPictureLots", new[] { "LotPicture_LotPicture_id" });
            DropIndex("dbo.Figures", new[] { "FigurePicture_UserPicture_id" });
            DropColumn("dbo.Figures", "FigurePicture_UserPicture_id");
            DropTable("dbo.LotPictureLots");
            DropTable("dbo.FigurePictures");
            DropTable("dbo.LotPictures");
        }
    }
}
