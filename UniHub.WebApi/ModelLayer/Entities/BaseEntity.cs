using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}