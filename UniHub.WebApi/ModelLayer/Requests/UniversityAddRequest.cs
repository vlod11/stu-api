using System;
using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Requests
{
    public class UniversityAddRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullTitle { get; set; }
        [Required]
        [StringLength(7)]
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        [Required]
        public int CityId { get; set; }
    }
}