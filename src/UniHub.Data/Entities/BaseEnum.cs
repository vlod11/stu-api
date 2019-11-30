using System.ComponentModel.DataAnnotations;

namespace UniHub.Data.Entities
{
    public class BaseEnum
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
    }
}