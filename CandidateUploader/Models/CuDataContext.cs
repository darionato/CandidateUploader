using System.Data.Entity;

namespace CandidateUploader.Models
{
    public class CuDataContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }

        public DbSet<AttributeModel> Attributes { get; set; }
    }
}