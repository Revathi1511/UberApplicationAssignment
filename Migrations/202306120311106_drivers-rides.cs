namespace UberApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class driversrides : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        DriverID = c.Int(nullable: false, identity: true),
                        DriverFirstName = c.String(),
                        DriverLastName = c.String(),
                    })
                .PrimaryKey(t => t.DriverID);
            
            CreateTable(
                "dbo.DriverRides",
                c => new
                    {
                        Driver_DriverID = c.Int(nullable: false),
                        Ride_RideId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Driver_DriverID, t.Ride_RideId })
                .ForeignKey("dbo.Drivers", t => t.Driver_DriverID, cascadeDelete: true)
                .ForeignKey("dbo.Rides", t => t.Ride_RideId, cascadeDelete: true)
                .Index(t => t.Driver_DriverID)
                .Index(t => t.Ride_RideId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DriverRides", "Ride_RideId", "dbo.Rides");
            DropForeignKey("dbo.DriverRides", "Driver_DriverID", "dbo.Drivers");
            DropIndex("dbo.DriverRides", new[] { "Ride_RideId" });
            DropIndex("dbo.DriverRides", new[] { "Driver_DriverID" });
            DropTable("dbo.DriverRides");
            DropTable("dbo.Drivers");
        }
    }
}
