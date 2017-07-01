namespace CallCenter.API.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageAuthorIdNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "AuthorId" });
            AlterColumn("dbo.Messages", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Messages", "AuthorId");
            AddForeignKey("dbo.Messages", "AuthorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "AuthorId" });
            AlterColumn("dbo.Messages", "AuthorId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Messages", "AuthorId");
            AddForeignKey("dbo.Messages", "AuthorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
