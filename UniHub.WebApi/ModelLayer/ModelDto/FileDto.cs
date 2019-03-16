using System;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.ModelLayer.ModelDto
{
    public class FileDto
    {
        public string Name { get; set; }
        public EFileType FileType { get; set; }
        public string Url { get; set; }
    }
}