namespace WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdditionalInformations",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Informations", t => t.Id)
                .Index(t => t.Id);
            
           
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "ImagesWithResolutions_ImagesWithResolutionsID", "dbo.ImagesWithResolutions");
            DropForeignKey("dbo.ImagesWithResolutions", "product_ProductID", "dbo.Products");
            DropForeignKey("dbo.Reviews", "productId", "dbo.Products");
            DropForeignKey("dbo.ImagesWithResolutions", "productId", "dbo.Products");
            DropForeignKey("dbo.Images", "ImagesWithResolutionsId", "dbo.ImagesWithResolutions");
            DropForeignKey("dbo.AdditionalInformations", "Id", "dbo.Informations");
            DropIndex("dbo.Reviews", new[] { "productId" });
            DropIndex("dbo.ImagesWithResolutions", new[] { "product_ProductID" });
            DropIndex("dbo.ImagesWithResolutions", new[] { "productId" });
            DropIndex("dbo.Images", new[] { "ImagesWithResolutions_ImagesWithResolutionsID" });
            DropIndex("dbo.Images", new[] { "ImagesWithResolutionsId" });
            DropIndex("dbo.AdditionalInformations", new[] { "Id" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Products");
            DropTable("dbo.ImagesWithResolutions");
            DropTable("dbo.Images");
            DropTable("dbo.Informations");
            DropTable("dbo.AdditionalInformations");
        }
    }
}
