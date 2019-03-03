using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class File : BaseEntity
    {
        public string Path { get; set; }

        //Relation to Post
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}