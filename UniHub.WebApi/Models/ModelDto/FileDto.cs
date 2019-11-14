using UniHub.WebApi.Models.Enums;

namespace UniHub.WebApi.Models.ModelDto
{
    public class FileDto
    {
        public string Name { get; set; }
        public EFileType FileType { get; set; }
        public string Url { get; set; }
    }
}