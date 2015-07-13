using System.ComponentModel.DataAnnotations;

namespace CandidateUploader.Models
{
    public class AttributeModel
    {

        [Key]
        public int Id { get; set; }


        public virtual AttributeModel Parent { get; set; }


        [MaxLength(250)]
        public string Code { get; set; }


        [MaxLength(250)]
        public string Type { get; set; }


        public float Position { get; set; }


        public string Text { get; set; }

    }
}