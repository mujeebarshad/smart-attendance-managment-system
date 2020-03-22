namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendanceModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Subject = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            CreateIndex("dbo.Attendances", "StudentId");
            AddForeignKey("dbo.Attendances", "StudentId", "dbo.Students", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropTable("dbo.Attendances");
        }
    }
}
