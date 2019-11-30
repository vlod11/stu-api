using System.ComponentModel.DataAnnotations;

namespace UniHub.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}