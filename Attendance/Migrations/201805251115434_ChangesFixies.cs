namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesFixies : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Attendances", newName: "Proxies");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Proxies", newName: "Attendances");
        }
    }
}
