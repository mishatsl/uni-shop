namespace WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdditionalInformations", "ShortInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdditionalInformations", "ShortInfo");
        }
    }
}
