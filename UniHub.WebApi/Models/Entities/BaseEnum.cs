using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.Models.Entities
{
    public class BaseEnum
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
    }
}