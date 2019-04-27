using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class AnswerVote : BaseEntity
    {
        [Range(-1, 1)]
        public int Value { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public int AnswerId { get; set; }
        [ForeignKey(nameof(AnswerId))]
        public virtual Answer Answer { get; set; }
    }
}