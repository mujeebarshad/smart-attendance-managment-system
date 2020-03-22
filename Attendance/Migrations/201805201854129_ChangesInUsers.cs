namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesInUsers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "username", c => c.String());
            AlterColumn("dbo.Users", "userType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "userType", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "username", c => c.String(nullable: false));
        }
    }
}
