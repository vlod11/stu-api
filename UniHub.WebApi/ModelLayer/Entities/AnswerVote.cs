using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class AnswerVote : BaseEntity
    {
        public int UsersProfileId { get; set; }
        public int AnswerId { get; set; }
        [Range(-1, 1)]
        public int Value { get; set; }

        public virtual UsersProfile UsersProfile { get; set; }
        public virtual Answer Answer { get; set; }
    }
}