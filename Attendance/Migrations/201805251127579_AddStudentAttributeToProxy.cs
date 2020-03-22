namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentAttributeToProxy : DbMigration
    {
        public override void Up()
        {
            //CreateIndex("dbo.Proxies", "StudentId");
            //AddForeignKey("dbo.Proxies", "StudentId", "dbo.Students", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Proxies", "StudentId", "dbo.Students");
            DropIndex("dbo.Proxies", new[] { "StudentId" });
        }
    }
}
