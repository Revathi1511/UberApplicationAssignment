namespace UberApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class car : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        CarName = c.String(),
                        CarRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CarId);
            
            AddColumn("dbo.Rides", "RideNum", c => c.Int(nullable: false));
            AddColumn("dbo.Rides", "RideCar", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rides", "RideCar");
            DropColumn("dbo.Rides", "RideNum");
            DropTable("dbo.Cars");
        }
    }
}
