namespace CallCenter.API.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerEmployeeAndMessageTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        AccountBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        Status = c.Int(nullable: false),
                        ConversationId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConversationId = c.String(),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                        AuthorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employees", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "AuthorId" });
            DropIndex("dbo.Employees", new[] { "ApplicationUserId" });
            DropIndex("dbo.Customers", new[] { "ApplicationUserId" });
            DropTable("dbo.Messages");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
        }
    }
}
