using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using UniHub.WebApi.BLL.Helpers.Contract;
using UniHub.WebApi.Shared.Options;

namespace UniHub.WebApi.BLL.Helpers
{
    public class FolderHelper : IFolderHelper
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly FilesOptions _filesOptions;

        public FolderHelper(IHostingEnvironment hostingEnvironment, IOptions<FilesOptions> filesOptions)
        {
            _hostingEnvironment = hostingEnvironment;
            _filesOptions = filesOptions.Value;
        }

        public void CreateFilesFoldersIfNotExist()
        {
            var folderPath = Path.Combine(_hostingEnvironment.ContentRootPath, $"{_filesOptions.UploadFolder}");
            CheckOrCreateFolder(folderPath);
            CheckOrCreateFolder(Path.Combine(folderPath, $"{_filesOptions.InnerFolders.ImagesFolder}"));
            CheckOrCreateFolder(Path.Combine(folderPath, $"{_filesOptions.InnerFolders.FilesFolder}"));
        }

        private void CheckOrCreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}