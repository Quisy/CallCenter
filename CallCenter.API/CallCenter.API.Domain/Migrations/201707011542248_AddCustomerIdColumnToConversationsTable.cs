namespace CallCenter.API.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerIdColumnToConversationsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "CustomerId", c => c.Int());
            CreateIndex("dbo.Conversations", "CustomerId");
            AddForeignKey("dbo.Conversations", "CustomerId", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conversations", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Conversations", new[] { "CustomerId" });
            DropColumn("dbo.Conversations", "CustomerId");
        }
    }
}
