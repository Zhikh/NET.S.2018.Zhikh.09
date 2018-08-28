namespace Task2.ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Account.Account",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 50, unicode: false),
                        IsOpen = c.Boolean(nullable: false),
                        OwnerId = c.Int(nullable: false),
                        Invoice = c.Decimal(nullable: false, storeType: "money"),
                        Bonuses = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountType", t => t.TypeId)
                .ForeignKey("Person.Person", t => t.OwnerId)
                .Index(t => t.OwnerId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.AccountType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        DepositBonusCost = c.Decimal(nullable: false, storeType: "money"),
                        WithdrawBonusSost = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Person.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Person.ContactData",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("Person.Person", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "Person.Passport",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        PassportSeries = c.String(nullable: false, maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("Person.Person", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Person.Passport", "PersonId", "Person.Person");
            DropForeignKey("Person.ContactData", "PersonId", "Person.Person");
            DropForeignKey("Account.Account", "OwnerId", "Person.Person");
            DropForeignKey("Account.Account", "TypeId", "dbo.AccountType");
            DropIndex("Person.Passport", new[] { "PersonId" });
            DropIndex("Person.ContactData", new[] { "PersonId" });
            DropIndex("Account.Account", new[] { "TypeId" });
            DropIndex("Account.Account", new[] { "OwnerId" });
            DropTable("dbo.sysdiagrams");
            DropTable("Person.Passport");
            DropTable("Person.ContactData");
            DropTable("Person.Person");
            DropTable("dbo.AccountType");
            DropTable("Account.Account");
        }
    }
}
