namespace MVCTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalendarDates",
                c => new
                    {
                        CalendarDateID = c.Int(nullable: false, identity: true),
                        CalendarDate1 = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CalendarDateID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmploymentType = c.Int(nullable: false),
                        CalendarDateID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CalendarDates", t => t.CalendarDateID)
                .Index(t => t.CalendarDateID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "CalendarDateID", "dbo.CalendarDates");
            DropIndex("dbo.Students", new[] { "CalendarDateID" });
            DropTable("dbo.Students");
            DropTable("dbo.CalendarDates");
        }
    }
}
