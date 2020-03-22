namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttributesToStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "rollno", c => c.String());
            AddColumn("dbo.Students", "img_path", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "img_path");
            DropColumn("dbo.Students", "rollno");
        }
    }
}
