namespace UniHub.WebApi.Shared.Options
{
    public class FilesOptions
    {
        public string UploadFolder { get; set; }
        public InnerFolders InnerFolders { get; set; }
    }

    public class InnerFolders
    {
        public string ImagesFolder { get; set; }
        public string FilesFolder { get; set; }
    }
}