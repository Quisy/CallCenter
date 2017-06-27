namespace CallCenter.API.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFacebookMessageIdColumnInMessages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "FacebookMessageId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "FacebookMessageId");
        }
    }
}
