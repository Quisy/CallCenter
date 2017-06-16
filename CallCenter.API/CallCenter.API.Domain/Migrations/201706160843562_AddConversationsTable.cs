namespace CallCenter.API.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConversationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacebookConversationId = c.String(),
                        AssignedEmployeeId = c.Int(),
                        Status = c.Int(nullable: false),
                        ProcessTask = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.AssignedEmployeeId)
                .Index(t => t.AssignedEmployeeId);
            
            AlterColumn("dbo.Messages", "ConversationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "ConversationId");
            AddForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations", "Id", cascadeDelete: true);
            DropColumn("dbo.Employees", "ConversationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "ConversationId", c => c.String());
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Conversations", "AssignedEmployeeId", "dbo.Employees");
            DropIndex("dbo.Messages", new[] { "ConversationId" });
            DropIndex("dbo.Conversations", new[] { "AssignedEmployeeId" });
            AlterColumn("dbo.Messages", "ConversationId", c => c.String(nullable: false));
            DropTable("dbo.Conversations");
        }
    }
}
