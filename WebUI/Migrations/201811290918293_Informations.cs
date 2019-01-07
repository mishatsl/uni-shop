namespace WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Informations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        alt = c.String(),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        width = c.Int(nullable: false),
                        height = c.Int(nullable: false),
                        ImagesWithResolutionsId = c.Int(),
                        ImagesWithResolutions_ImagesWithResolutionsID = c.Int(),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.ImagesWithResolutions", t => t.ImagesWithResolutionsId, cascadeDelete: true)
                .ForeignKey("dbo.ImagesWithResolutions", t => t.ImagesWithResolutions_ImagesWithResolutionsID)
                .Index(t => t.ImagesWithResolutionsId)
                .Index(t => t.ImagesWithResolutions_ImagesWithResolutionsID);
            
            CreateTable(
                "dbo.ImagesWithResolutions",
                c => new
                    {
                        ImagesWithResolutionsID = c.Int(nullable: false, identity: true),
                        productId = c.Int(),
                        product_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.ImagesWithResolutionsID)
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.product_ProductID)
                .Index(t => t.productId)
                .Index(t => t.product_ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Сategory = c.String(),
                        Brand = c.String(),
                        Description = c.String(),
                        Color = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PreviousPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InStock = c.Boolean(nullable: false),
                        ProductRating = c.Int(nullable: false),
                        Details = c.String(),
                        feature = c.String(),
                        TopSelling = c.Boolean(nullable: false),
                        New = c.Boolean(nullable: false),
                        average_rating = c.Single(nullable: false),
                        MainImageIndex = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Author = c.String(),
                        Description = c.String(),
                        Email = c.String(),
                        productId = c.Int(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Products", t => t.productId)
                .Index(t => t.productId);
            
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Head = c.String(),
                        HTMLContent = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUserLogins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Hometown = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Images", "ImagesWithResolutions_ImagesWithResolutionsID", "dbo.ImagesWithResolutions");
            DropForeignKey("dbo.ImagesWithResolutions", "product_ProductID", "dbo.Products");
            DropForeignKey("dbo.Reviews", "productId", "dbo.Products");
            DropForeignKey("dbo.ImagesWithResolutions", "productId", "dbo.Products");
            DropForeignKey("dbo.Images", "ImagesWithResolutionsId", "dbo.ImagesWithResolutions");
            DropIndex("dbo.Reviews", new[] { "productId" });
            DropIndex("dbo.ImagesWithResolutions", new[] { "product_ProductID" });
            DropIndex("dbo.ImagesWithResolutions", new[] { "productId" });
            DropIndex("dbo.Images", new[] { "ImagesWithResolutions_ImagesWithResolutionsID" });
            DropIndex("dbo.Images", new[] { "ImagesWithResolutionsId" });
            DropTable("dbo.Information");
            DropTable("dbo.Reviews");
            DropTable("dbo.Products");
            DropTable("dbo.ImagesWithResolutions");
            DropTable("dbo.Images");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.AspNetUserRoles", "RoleId");
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            CreateIndex("dbo.AspNetRoles", "Name", unique: true, name: "RoleNameIndex");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}
