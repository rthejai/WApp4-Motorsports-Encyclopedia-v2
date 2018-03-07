namespace MotorsportsEncyclopedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1stmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Car",
                c => new
                    {
                        CarID = c.Int(nullable: false, identity: true),
                        CarYear = c.Int(nullable: false),
                        CarMake = c.String(maxLength: 100),
                        CarName = c.String(maxLength: 100),
                        CarDescription = c.String(),
                    })
                .PrimaryKey(t => t.CarID);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        EnrollmentID = c.Int(nullable: false, identity: true),
                        TrackID = c.Int(),
                        CarID = c.Int(),
                        LapTime = c.String(),
                    })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Car", t => t.CarID)
                .ForeignKey("dbo.Track", t => t.TrackID)
                .Index(t => t.TrackID)
                .Index(t => t.CarID);
            
            CreateTable(
                "dbo.Track",
                c => new
                    {
                        TrackID = c.Int(nullable: false),
                        TrackName = c.String(maxLength: 100),
                        TrackLocation = c.String(maxLength: 100),
                        Company_CompanyID = c.Int(),
                    })
                .PrimaryKey(t => t.TrackID)
                .ForeignKey("dbo.Company", t => t.Company_CompanyID)
                .Index(t => t.Company_CompanyID);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 100),
                        CompanyLocation = c.String(maxLength: 100),
                        StartDate = c.DateTime(),
                        TeacherID = c.Int(),
                        Driver_ID = c.Int(),
                    })
                .PrimaryKey(t => t.CompanyID)
                .ForeignKey("dbo.Person", t => t.TeacherID)
                .ForeignKey("dbo.Person", t => t.Driver_ID)
                .Index(t => t.TeacherID)
                .Index(t => t.Driver_ID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(maxLength: 100),
                        FirstName = c.String(maxLength: 100),
                        HireDate = c.DateTime(),
                        HireDate1 = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LocationAssignment",
                c => new
                    {
                        TeacherID = c.Int(nullable: false),
                        Location = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.TeacherID)
                .ForeignKey("dbo.Person", t => t.TeacherID)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.TrackTeacher",
                c => new
                    {
                        TrackID = c.Int(nullable: false),
                        TeacherID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrackID, t.TeacherID })
                .ForeignKey("dbo.Track", t => t.TrackID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.TrackID)
                .Index(t => t.TeacherID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Company", "Driver_ID", "dbo.Person");
            DropForeignKey("dbo.TrackTeacher", "TeacherID", "dbo.Person");
            DropForeignKey("dbo.TrackTeacher", "TrackID", "dbo.Track");
            DropForeignKey("dbo.Enrollment", "TrackID", "dbo.Track");
            DropForeignKey("dbo.Track", "Company_CompanyID", "dbo.Company");
            DropForeignKey("dbo.Company", "TeacherID", "dbo.Person");
            DropForeignKey("dbo.LocationAssignment", "TeacherID", "dbo.Person");
            DropForeignKey("dbo.Enrollment", "CarID", "dbo.Car");
            DropIndex("dbo.TrackTeacher", new[] { "TeacherID" });
            DropIndex("dbo.TrackTeacher", new[] { "TrackID" });
            DropIndex("dbo.LocationAssignment", new[] { "TeacherID" });
            DropIndex("dbo.Company", new[] { "Driver_ID" });
            DropIndex("dbo.Company", new[] { "TeacherID" });
            DropIndex("dbo.Track", new[] { "Company_CompanyID" });
            DropIndex("dbo.Enrollment", new[] { "CarID" });
            DropIndex("dbo.Enrollment", new[] { "TrackID" });
            DropTable("dbo.TrackTeacher");
            DropTable("dbo.LocationAssignment");
            DropTable("dbo.Person");
            DropTable("dbo.Company");
            DropTable("dbo.Track");
            DropTable("dbo.Enrollment");
            DropTable("dbo.Car");
        }
    }
}
