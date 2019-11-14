using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.Models.Entities
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? DeletedAtUtc { get; set; }
        
        public int FileTypeId { get; set; }
        [ForeignKey(nameof(FileTypeId))]
        public FileType Type { get; set; }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
    }
}