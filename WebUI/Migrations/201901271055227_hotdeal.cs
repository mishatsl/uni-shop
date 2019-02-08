namespace WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotdeal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotDeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HotDealActionStartTime = c.DateTime(nullable: false),
                        HotDealActionEndTime = c.DateTime(nullable: false),
                        HotDealTime = c.DateTime(nullable: false),
                        HotDealToProducts = c.String(),
                        IsHotDeal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HotDeals");
        }
    }
}
