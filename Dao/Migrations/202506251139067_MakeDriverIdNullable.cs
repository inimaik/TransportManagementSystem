namespace Dao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDriverIdNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trips", "DriverId", "dbo.Drivers");
            DropIndex("dbo.Trips", new[] { "DriverId" });
            AlterColumn("dbo.Trips", "DriverId", c => c.Int());
            CreateIndex("dbo.Trips", "DriverId");
            AddForeignKey("dbo.Trips", "DriverId", "dbo.Drivers", "DriverId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "DriverId", "dbo.Drivers");
            DropIndex("dbo.Trips", new[] { "DriverId" });
            AlterColumn("dbo.Trips", "DriverId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trips", "DriverId");
            AddForeignKey("dbo.Trips", "DriverId", "dbo.Drivers", "DriverId", cascadeDelete: true);
        }
    }
}
