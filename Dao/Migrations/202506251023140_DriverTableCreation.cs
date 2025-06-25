namespace Dao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverTableCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        DriverId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LicenseNumber = c.String(),
                        PhoneNumber = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.DriverId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Drivers");
        }
    }
}
