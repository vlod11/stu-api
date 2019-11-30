using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using UniHub.Common.Enums;
using UniHub.Common.Helpers.Contract;
using UniHub.Common.Options;

namespace UniHub.Common.Helpers
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
            CheckOrCreateFolder(Path.Combine(folderPath, $"{_filesOptions.InnerFolders.DefaultImagesFolder}"));

            foreach (EFileType @enum in Enum.GetValues(typeof(EFileType)))
            {
                var enumString = @enum.ToString();
                CheckOrCreateFolder(Path.Combine(folderPath, $"{_filesOptions.InnerFolders.FilesFolder}/{enumString}"));
            }
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