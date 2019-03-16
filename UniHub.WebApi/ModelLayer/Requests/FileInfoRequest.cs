using System.ComponentModel.DataAnnotations;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.ModelLayer.Requests
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