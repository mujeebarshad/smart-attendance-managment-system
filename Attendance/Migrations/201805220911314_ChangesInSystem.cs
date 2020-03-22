namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesInSystem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Instructors", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Instructors", "password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instructors", "password", c => c.String());
            AlterColumn("dbo.Instructors", "name", c => c.String());
        }
    }
}
