using System.Collections.Generic;

namespace UniHub.WebApi.Models.Entities
{
    public class FileType : BaseEnum
    {
        public virtual ICollection<File> Files { get; set; }
    }
}