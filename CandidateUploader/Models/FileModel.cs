using System;
using System.ComponentModel.DataAnnotations;

namespace CandidateUploader.Models
{

    public enum FileStatus
    {
        None = 0,
        Approved = 1
    }

    public class FileModel
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Author { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        [MaxLength(100)]
        public string FreeTags { get; set; }

        public DateTime Created { get; set; }

        public FileStatus State { get; set; }

        [MaxLength(255)]
        public string FileName { get; set; }

        [MaxLength(100)]
        public string FileType { get; set; }

        public byte[] File { get; set; }

        public byte[] ImageThumbnail { get; set; }

        public byte[] ImageList { get; set; }

        public byte[] ImageDetail { get; set; }

        public virtual AttributeModel Contest { get; set; }


    }
}