using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class PostAction : BaseEntity
    {
        public int UsersProfileId { get; set; }

        [ForeignKey(nameof(UsersProfileId))]
        public virtual UsersProfile UsersProfile { get; set; }

        public int ActionTypeId { get; set; }
        [ForeignKey(nameof(ActionTypeId))]
        public virtual PostActionType ActionType { get; set; }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
    }
}