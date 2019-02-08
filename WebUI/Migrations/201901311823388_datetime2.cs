namespace WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HotDeals", "HotDealActionStartTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.HotDeals", "HotDealActionEndTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.HotDeals", "HotDealTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Reviews", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.HotDeals", "HotDealTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.HotDeals", "HotDealActionEndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.HotDeals", "HotDealActionStartTime", c => c.DateTime(nullable: false));
        }
    }
}
