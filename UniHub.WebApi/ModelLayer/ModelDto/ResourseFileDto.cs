using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.ModelLayer.ModelDto
{
    public class FileDto
    {
        public EFileType FileType { get; set; }
        public string Url { get; set; }
    }
}