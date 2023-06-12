namespace UberApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ride : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rides", "RideCar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rides", "RideCar", c => c.Int(nullable: false));
        }
    }
}
