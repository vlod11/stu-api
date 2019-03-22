using System;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.ModelLayer.ModelDto
{
    public class FileDto
    {
        public string Name { get; set; }
        public int FileType { get; set; }
        public string Url { get; set; }
    }
}