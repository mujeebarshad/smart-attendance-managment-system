namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePathToInstructor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "image_path", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instructors", "image_path");
        }
    }
}
