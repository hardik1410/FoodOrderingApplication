namespace FoodOrder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.FoodItems",
                c => new
                    {
                        FoodId = c.Int(nullable: false, identity: true),
                        FoodName = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FoodId)
                .ForeignKey("dbo.FCategories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ResCats",
                c => new
                    {
                        RId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        FoodId = c.Int(nullable: false),
                        FoodPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RId, t.CategoryId, t.FoodId })
                .ForeignKey("dbo.FCategories", t => t.CategoryId)
                .ForeignKey("dbo.FoodItems", t => t.FoodId)
                .ForeignKey("dbo.Restaurants", t => t.RId)
                .Index(t => t.RId)
                .Index(t => t.CategoryId)
                .Index(t => t.FoodId);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RId = c.Int(nullable: false, identity: true),
                        RName = c.String(),
                        RLocation = c.String(),
                        RContact = c.String(),
                        OpenCloseTime = c.String(),
                    })
                .PrimaryKey(t => t.RId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Password = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResCats", "RId", "dbo.Restaurants");
            DropForeignKey("dbo.ResCats", "FoodId", "dbo.FoodItems");
            DropForeignKey("dbo.ResCats", "CategoryId", "dbo.FCategories");
            DropForeignKey("dbo.FoodItems", "CategoryId", "dbo.FCategories");
            DropIndex("dbo.ResCats", new[] { "FoodId" });
            DropIndex("dbo.ResCats", new[] { "CategoryId" });
            DropIndex("dbo.ResCats", new[] { "RId" });
            DropIndex("dbo.FoodItems", new[] { "CategoryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Restaurants");
            DropTable("dbo.ResCats");
            DropTable("dbo.FoodItems");
            DropTable("dbo.FCategories");
        }
    }
}
