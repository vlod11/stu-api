using System.ComponentModel.DataAnnotations;
using UniHub.WebApi.Models.Enums;

namespace UniHub.WebApi.Models.Requests
{
    public class FileInfoRequest
    {
        public EFileType FileType { get; set; }

        [Required]
        public string Url { get; set; }
        [Required]
        public string Name { get; set; }
    }
}