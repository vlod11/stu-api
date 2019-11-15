using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.Models.Entities
{
    public class File : BaseEntity
    {
        public File(string name = null, string path = null, DateTime createdAtUtc = default, DateTime? deletedAtUtc = default, int fileTypeId = default, FileType type = null, int postId = default, Post post = null)
        {
            Name = name;
            Path = path;
            CreatedAtUtc = createdAtUtc;
            DeletedAtUtc = deletedAtUtc;
            FileTypeId = fileTypeId;
            Type = type;
            PostId = postId;
            Post = post;
        }

        public File()
        {
        }

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