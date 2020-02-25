using System;
using System.IO;
using System.IO.Abstractions;
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
        private readonly IFileSystem _fileSystem;

        public FolderHelper(IHostingEnvironment hostingEnvironment, IOptions<FilesOptions> filesOptions,
            IFileSystem fileSystem)
        {
            _hostingEnvironment = hostingEnvironment;
            _filesOptions = filesOptions.Value;
            _fileSystem = fileSystem;
        }

        public void CreateFilesFoldersIfNotExist()
        {
            var folderPath =
                _fileSystem.Path.Combine(_hostingEnvironment.ContentRootPath, $"{_filesOptions.UploadFolder}");
            CheckOrCreateFolder(folderPath);
            CheckOrCreateFolder(_fileSystem.Path.Combine(folderPath, $"{_filesOptions.InnerFolders.ImagesFolder}"));
            CheckOrCreateFolder(_fileSystem.Path.Combine(folderPath, $"{_filesOptions.InnerFolders.FilesFolder}"));
            CheckOrCreateFolder(_fileSystem.Path.Combine(
                folderPath,
                $"{_filesOptions.InnerFolders.DefaultImagesFolder}"));

            foreach (EFileType @enum in Enum.GetValues(typeof(EFileType)))
            {
                var enumString = @enum.ToString();
                CheckOrCreateFolder(_fileSystem.Path.Combine(
                    folderPath,
                    $"{_filesOptions.InnerFolders.FilesFolder}/{enumString}"));
            }
        }

        private void CheckOrCreateFolder(string folderPath)
        {
            if (!_fileSystem.Directory.Exists(folderPath))
            {
                _fileSystem.Directory.CreateDirectory(folderPath);
            }
        }
    }
}