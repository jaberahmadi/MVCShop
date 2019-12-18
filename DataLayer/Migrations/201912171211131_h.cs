namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        ResellersId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        OstanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Province", t => t.OstanId, cascadeDelete: true)
                .Index(t => t.OstanId);
            
            CreateTable(
                "dbo.Province",
                c => new
                    {
                        OstanId = c.Int(nullable: false, identity: true),
                        ResellersId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.OstanId);
            
            CreateTable(
                "dbo.Resellers",
                c => new
                    {
                        ResellerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 500),
                        City_CityId = c.Int(),
                        Ostan_OstanId = c.Int(),
                    })
                .PrimaryKey(t => t.ResellerId)
                .ForeignKey("dbo.Cities", t => t.City_CityId)
                .ForeignKey("dbo.Province", t => t.Ostan_OstanId)
                .Index(t => t.City_CityId)
                .Index(t => t.Ostan_OstanId);
            
            CreateTable(
                "dbo.FactorItems",
                c => new
                    {
                        FactorItemId = c.Int(nullable: false, identity: true),
                        FactorId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Qty = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.FactorItemId)
                .ForeignKey("dbo.Factors", t => t.FactorId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.FactorId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Factors",
                c => new
                    {
                        FactorId = c.Int(nullable: false, identity: true),
                        BuyDate = c.DateTime(nullable: false),
                        FlloweCode = c.String(maxLength: 50),
                        Description = c.String(maxLength: 400),
                        UserId = c.Int(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(nullable: false, maxLength: 50),
                        Mobile = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 200),
                        CodePosti = c.String(maxLength: 15),
                        IsFinish = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FactorId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        BirthDate = c.DateTime(),
                        Mobile = c.String(maxLength: 50),
                        Tell = c.String(maxLength: 50),
                        Gender = c.Boolean(nullable: false),
                        Status = c.Byte(nullable: false),
                        Roles = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Summery = c.String(nullable: false),
                        GroupId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Url = c.String(nullable: false, maxLength: 100),
                        Keywords = c.String(maxLength: 300),
                        Description = c.String(maxLength: 500),
                        Tags = c.String(maxLength: 200),
                        Like = c.Int(nullable: false),
                        Dislike = c.Int(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentId = c.Int(),
                        Group1_GroupId = c.Int(),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.Groups", t => t.Group1_GroupId)
                .Index(t => t.Group1_GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Groups", "Group1_GroupId", "dbo.Groups");
            DropForeignKey("dbo.FactorItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Factors", "UserId", "dbo.Users");
            DropForeignKey("dbo.FactorItems", "FactorId", "dbo.Factors");
            DropForeignKey("dbo.Resellers", "Ostan_OstanId", "dbo.Province");
            DropForeignKey("dbo.Resellers", "City_CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "OstanId", "dbo.Province");
            DropIndex("dbo.Groups", new[] { "Group1_GroupId" });
            DropIndex("dbo.Products", new[] { "GroupId" });
            DropIndex("dbo.Factors", new[] { "UserId" });
            DropIndex("dbo.FactorItems", new[] { "ProductId" });
            DropIndex("dbo.FactorItems", new[] { "FactorId" });
            DropIndex("dbo.Resellers", new[] { "Ostan_OstanId" });
            DropIndex("dbo.Resellers", new[] { "City_CityId" });
            DropIndex("dbo.Cities", new[] { "OstanId" });
            DropTable("dbo.Groups");
            DropTable("dbo.Products");
            DropTable("dbo.Users");
            DropTable("dbo.Factors");
            DropTable("dbo.FactorItems");
            DropTable("dbo.Resellers");
            DropTable("dbo.Province");
            DropTable("dbo.Cities");
        }
    }
}
