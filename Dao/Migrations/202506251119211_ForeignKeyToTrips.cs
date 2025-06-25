namespace Dao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyToTrips : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "DriverId", c => c.Int());
            CreateIndex("dbo.Trips", "DriverId");
            AddForeignKey("dbo.Trips", "DriverId", "dbo.Drivers", "DriverId");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "DriverId", "dbo.Drivers");
            DropIndex("dbo.Trips", new[] { "DriverId" });
            DropColumn("dbo.Trips", "DriverId");
        }
    }
}
