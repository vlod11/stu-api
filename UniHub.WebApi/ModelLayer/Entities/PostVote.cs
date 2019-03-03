using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class PostVote : BaseEntity
    {
        public int UsersProfileId { get; set; }
        public int PostId { get; set; }
        [Range(-1, 1)]
        public int Value { get; set; }

        public virtual UsersProfile UsersProfile { get; set; }
        public virtual Post Post { get; set; }
    }
}