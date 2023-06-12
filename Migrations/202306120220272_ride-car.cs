namespace UberApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ridecar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rides", "CarID", c => c.Int(nullable: false));
            CreateIndex("dbo.Rides", "CarID");
            AddForeignKey("dbo.Rides", "CarID", "dbo.Cars", "CarId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rides", "CarID", "dbo.Cars");
            DropIndex("dbo.Rides", new[] { "CarID" });
            DropColumn("dbo.Rides", "CarID");
        }
    }
}
