using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}