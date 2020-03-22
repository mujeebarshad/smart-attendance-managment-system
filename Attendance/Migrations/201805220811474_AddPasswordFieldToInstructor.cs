namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPasswordFieldToInstructor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instructors", "password");
        }
    }
}
