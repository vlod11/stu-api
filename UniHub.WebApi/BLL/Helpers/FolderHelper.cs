using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using UniHub.WebApi.BLL.Helpers.Contract;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.Common.Options;

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