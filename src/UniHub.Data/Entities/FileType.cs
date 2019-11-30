using System.Collections.Generic;

namespace UniHub.Data.Entities
{
    public class FileType : BaseEnum
    {
        public virtual ICollection<File> Files { get; set; }
    }
}