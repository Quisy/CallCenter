namespace CallCenter.API.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProcessInstantionIdColumnInConversations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "ProcessInstanceId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "ProcessInstanceId");
        }
    }
}
