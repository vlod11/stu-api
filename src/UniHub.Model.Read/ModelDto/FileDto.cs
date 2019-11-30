using UniHub.Common.Enums;

namespace UniHub.Model.Read.ModelDto
{
    public class FileDto
    {
        public string Name { get; set; }
        public EFileType FileType { get; set; }
        public string Url { get; set; }
    }
}