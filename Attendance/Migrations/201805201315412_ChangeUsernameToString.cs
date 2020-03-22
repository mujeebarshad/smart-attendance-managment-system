namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUsernameToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "username", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "username", c => c.Int(nullable: false));
        }
    }
}
