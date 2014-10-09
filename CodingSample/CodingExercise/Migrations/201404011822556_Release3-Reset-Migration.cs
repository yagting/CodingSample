namespace CodingExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Release3ResetMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Awards",
                c => new
                    {
                        AwardID = c.Int(nullable: false, identity: true),
                        RootID = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Birth = c.DateTime(),
                        AwardName = c.String(maxLength: 100),
                        AwardBy = c.String(maxLength: 100),
                        AwardYear = c.Int(),
                    })
                .PrimaryKey(t => t.AwardID);
            
            CreateTable(
                "dbo.Contribs",
                c => new
                    {
                        ContribID = c.Int(nullable: false, identity: true),
                        RootID = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Name = c.String(maxLength: 100),
                        Birth = c.DateTime(),
                    })
                .PrimaryKey(t => t.ContribID);
            
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        DeveloperID = c.Int(nullable: false, identity: true),
                        RootID = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        AKA = c.String(maxLength: 100),
                        Title = c.String(maxLength: 50),
                        Birth = c.DateTime(),
                        Death = c.DateTime(),
                    })
                .PrimaryKey(t => t.DeveloperID);
            
            CreateTable(
                "dbo.ReportBatches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RunDate = c.DateTime(nullable: false),
                        TotalDocumentRead = c.Long(nullable: false),
                        TotalDeveloperWritten = c.Long(nullable: false),
                        TotalAwardWritten = c.Long(nullable: false),
                        TotalContribsWritten = c.Long(nullable: false),
                        User = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReportBatches");
            DropTable("dbo.Developers");
            DropTable("dbo.Contribs");
            DropTable("dbo.Awards");
        }
    }
}
