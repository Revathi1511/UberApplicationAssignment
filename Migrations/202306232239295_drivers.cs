namespace UberApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drivers : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DriverRides", newName: "RideDrivers");
            DropPrimaryKey("dbo.RideDrivers");
            AddPrimaryKey("dbo.RideDrivers", new[] { "Ride_RideId", "Driver_DriverID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RideDrivers");
            AddPrimaryKey("dbo.RideDrivers", new[] { "Driver_DriverID", "Ride_RideId" });
            RenameTable(name: "dbo.RideDrivers", newName: "DriverRides");
        }
    }
}
