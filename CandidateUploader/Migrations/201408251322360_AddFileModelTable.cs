namespace CandidateUploader.Migrations
{

    using System.Data.Entity.Migrations;
    
    public partial class AddFileModelTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        Author = c.String(maxLength: 50),
                        Email = c.String(maxLength: 100),
                        Location = c.String(maxLength: 100),
                        FreeTags = c.String(maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FileModels");
        }
    }
}
