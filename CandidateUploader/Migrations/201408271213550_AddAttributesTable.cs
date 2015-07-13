namespace CandidateUploader.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddAttributesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttributeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 250),
                        Type = c.String(maxLength: 250),
                        Position = c.Single(nullable: false),
                        Text = c.String(),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AttributeModels", t => t.Parent_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.Position, "AttributesPositionIndex")
                .Index(t => t.Type, "AttributesTypeIndex");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.AttributeModels", "AttributesTypeIndex");
            DropIndex("dbo.AttributeModels", "AttributesPositionIndex");
            DropForeignKey("dbo.AttributeModels", "Parent_Id", "dbo.AttributeModels");
            DropIndex("dbo.AttributeModels", new[] { "Parent_Id" });
            DropTable("dbo.AttributeModels");
        }
    }
}
