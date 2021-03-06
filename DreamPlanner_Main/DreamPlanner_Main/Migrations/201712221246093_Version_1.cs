namespace DreamPlanner_Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        UserEmail = c.String(nullable: false),
                        CityId = c.Int(nullable: false),
                        UserStreetAddress = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        UserPassword = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        ContactOrNot = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.Halls",
                c => new
                    {
                        HallId = c.Int(nullable: false, identity: true),
                        HallName = c.String(nullable: false),
                        HallCapacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HallId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        ThemeId = c.Int(nullable: false),
                        HallId = c.Int(nullable: false),
                        ReservationDate = c.DateTime(nullable: false),
                        TotalRent = c.Int(nullable: false),
                        ReservationCode = c.String(),
                        AdvancePayment = c.Boolean(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Halls", t => t.HallId, cascadeDelete: true)
                .ForeignKey("dbo.Themes", t => t.ThemeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ThemeId)
                .Index(t => t.HallId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        ThemeId = c.Int(nullable: false, identity: true),
                        ThemeName = c.String(),
                        PartyTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ThemeId)
                .ForeignKey("dbo.PartyTypes", t => t.PartyTypeId, cascadeDelete: true)
                .Index(t => t.PartyTypeId);
            
            CreateTable(
                "dbo.PartyTypes",
                c => new
                    {
                        PartyTypeId = c.Int(nullable: false, identity: true),
                        PartyTypeName = c.String(),
                    })
                .PrimaryKey(t => t.PartyTypeId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        RatingName = c.String(),
                    })
                .PrimaryKey(t => t.RatingId);
            
            CreateTable(
                "dbo.UserExperiences",
                c => new
                    {
                        UserExperienceId = c.Int(nullable: false, identity: true),
                        RatingId = c.Int(nullable: false),
                        ExperienceDescription = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserExperienceId)
                .ForeignKey("dbo.Ratings", t => t.RatingId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RatingId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserExperiences", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserExperiences", "RatingId", "dbo.Ratings");
            DropForeignKey("dbo.Reservations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reservations", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.Themes", "PartyTypeId", "dbo.PartyTypes");
            DropForeignKey("dbo.Reservations", "HallId", "dbo.Halls");
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropIndex("dbo.UserExperiences", new[] { "UserId" });
            DropIndex("dbo.UserExperiences", new[] { "RatingId" });
            DropIndex("dbo.Themes", new[] { "PartyTypeId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropIndex("dbo.Reservations", new[] { "HallId" });
            DropIndex("dbo.Reservations", new[] { "ThemeId" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropTable("dbo.UserExperiences");
            DropTable("dbo.Ratings");
            DropTable("dbo.PartyTypes");
            DropTable("dbo.Themes");
            DropTable("dbo.Reservations");
            DropTable("dbo.Halls");
            DropTable("dbo.Contacts");
            DropTable("dbo.Users");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
