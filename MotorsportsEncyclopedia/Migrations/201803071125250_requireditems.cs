namespace MotorsportsEncyclopedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requireditems : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Car", "CarMake", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Car", "CarName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Track", "TrackName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Track", "TrackLocation", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Company", "CompanyName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Person", "LastName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Person", "FirstName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Person", "FirstName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Person", "LastName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Company", "CompanyName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Track", "TrackLocation", c => c.String(maxLength: 100));
            AlterColumn("dbo.Track", "TrackName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Car", "CarName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Car", "CarMake", c => c.String(maxLength: 100));
        }
    }
}
