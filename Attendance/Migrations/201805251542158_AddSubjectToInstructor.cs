namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubjectToInstructor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "subject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instructors", "subject");
        }
    }
}
