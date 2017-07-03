namespace CallCenter.API.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsSystemMessageColumnToMessagesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "IsSystemMessage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "IsSystemMessage");
        }
    }
}
