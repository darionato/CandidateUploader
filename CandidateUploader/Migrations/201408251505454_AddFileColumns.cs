namespace CandidateUploader.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddFileColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileModels", "FileName", c => c.String(maxLength: 255));
            AddColumn("dbo.FileModels", "FileType", c => c.String(maxLength: 100));
            AddColumn("dbo.FileModels", "File", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileModels", "File");
            DropColumn("dbo.FileModels", "FileType");
            DropColumn("dbo.FileModels", "FileName");
        }
    }
}
