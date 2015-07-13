namespace CandidateUploader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagesColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileModels", "ImageThumbnail", c => c.Binary());
            AddColumn("dbo.FileModels", "ImageList", c => c.Binary());
            AddColumn("dbo.FileModels", "ImageDetail", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileModels", "ImageDetail");
            DropColumn("dbo.FileModels", "ImageList");
            DropColumn("dbo.FileModels", "ImageThumbnail");
        }
    }
}
