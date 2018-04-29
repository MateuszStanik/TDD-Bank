namespace UnitOfWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        NRB = c.Long(nullable: false),
                        Ammount = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.TransactionHistory",
                c => new
                    {
                        TransactionLog = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Comment = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsIncome = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionLog);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "UserId", "dbo.Users");
            DropIndex("dbo.Accounts", new[] { "UserId" });
            DropTable("dbo.TransactionHistory");
            DropTable("dbo.Users");
            DropTable("dbo.Accounts");
        }
    }
}
