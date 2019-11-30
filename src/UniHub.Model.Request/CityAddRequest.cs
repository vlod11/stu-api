using System.ComponentModel.DataAnnotations;

namespace UniHub.Model.Request
{
    public class CityAddRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}