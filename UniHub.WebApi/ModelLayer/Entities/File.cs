using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using UniHub.WebApi.ModelLayer.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public int FileTypeId { get; set; }
        [ForeignKey(nameof(FileTypeId))]
        public FileType Type { get; set; }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
    }
}