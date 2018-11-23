namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LotPictureLots", "LotPicture_LotPicture_id", "dbo.LotPictures");
            DropForeignKey("dbo.LotPictureLots", "Lot_Lot_id", "dbo.Lots");
            DropForeignKey("dbo.Figures", "FigurePicture_UserPicture_id", "dbo.FigurePictures");
            DropIndex("dbo.Figures", new[] { "FigurePicture_UserPicture_id" });
            DropIndex("dbo.LotPictureLots", new[] { "LotPicture_LotPicture_id" });
            DropIndex("dbo.LotPictureLots", new[] { "Lot_Lot_id" });
            AddColumn("dbo.Lots", "PictureUrl", c => c.String());
            DropColumn("dbo.Figures", "FigurePicture_UserPicture_id");
            DropTable("dbo.LotPictures");
            DropTable("dbo.FigurePictures");
            DropTable("dbo.LotPictureLots");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LotPictureLots",
                c => new
                    {
                        LotPicture_LotPicture_id = c.Int(nullable: false),
                        Lot_Lot_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LotPicture_LotPicture_id, t.Lot_Lot_id });
            
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
                "dbo.LotPictures",
                c => new
                    {
                        LotPicture_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.LotPicture_id);
            
            AddColumn("dbo.Figures", "FigurePicture_UserPicture_id", c => c.Int());
            DropColumn("dbo.Lots", "PictureUrl");
            CreateIndex("dbo.LotPictureLots", "Lot_Lot_id");
            CreateIndex("dbo.LotPictureLots", "LotPicture_LotPicture_id");
            CreateIndex("dbo.Figures", "FigurePicture_UserPicture_id");
            AddForeignKey("dbo.Figures", "FigurePicture_UserPicture_id", "dbo.FigurePictures", "UserPicture_id");
            AddForeignKey("dbo.LotPictureLots", "Lot_Lot_id", "dbo.Lots", "Lot_id", cascadeDelete: true);
            AddForeignKey("dbo.LotPictureLots", "LotPicture_LotPicture_id", "dbo.LotPictures", "LotPicture_id", cascadeDelete: true);
        }
    }
}
