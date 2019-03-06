using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class PostVote : BaseEntity
    {
        [Range(-1, 1)]
        public int Value { get; set; }

        public int UsersProfileId { get; set; }
        
        [ForeignKey(nameof(UsersProfileId))]
        public virtual UsersProfile UsersProfile { get; set; }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
    }
}