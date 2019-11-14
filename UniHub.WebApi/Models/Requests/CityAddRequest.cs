using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.Models.Requests
{
    public class CityAddRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}