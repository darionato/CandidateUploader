namespace CandidateUploader.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddConstestToFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileModels", "Contest_Id", c => c.Int());
            CreateIndex("dbo.FileModels", "Contest_Id");
            AddForeignKey("dbo.FileModels", "Contest_Id", "dbo.AttributeModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileModels", "Contest_Id", "dbo.AttributeModels");
            DropIndex("dbo.FileModels", new[] { "Contest_Id" });
            DropColumn("dbo.FileModels", "Contest_Id");
        }
    }
}
