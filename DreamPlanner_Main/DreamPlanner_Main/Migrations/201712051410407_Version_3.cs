namespace DreamPlanner_Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version_3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactID = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        email = c.String(),
                        message = c.String(),
                        ContactOrNot = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContactID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
