using System.Collections.Generic;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class FileType : BaseEnum
    {
        public virtual ICollection<File> Files { get; set; }
    }
}