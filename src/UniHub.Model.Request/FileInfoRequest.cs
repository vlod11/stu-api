using System.ComponentModel.DataAnnotations;
using UniHub.Common.Enums;

namespace UniHub.Model.Request
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