namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForErrorClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "Id");
            DropColumn("dbo.Products", "Id");
        }
    }
}
