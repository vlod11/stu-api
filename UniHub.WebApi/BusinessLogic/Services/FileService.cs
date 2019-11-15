using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UniHub.WebApi.BusinessLogic.Services.Contract;
using UniHub.WebApi.Common.Options;
using UniHub.WebApi.Models.Enums;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;

namespace UniHub.WebApi.BusinessLogic.Services
{
    public class FileService : IFileService
    {
        //4 MB
        private const int _fileSize = 4000000;
        private const int _pixelSize = 640;

        private readonly FilesOptions _fileOptions;
        private readonly UrlsOptions _urlOptions;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        private readonly ILogger<FileService> _logger;

        private readonly List<string> _imageExtensions = new List<string>
        {
            ".png", ".jpeg", ".jpg"
        };

        private readonly List<string> _documentExtensions = new List<string>
        {
            ".pdf", ".doc", ".docx", ".xls", ".xlsx",
        };

        private readonly List<string> _mediaExtensions = new List<string>
        {
            ".mp3"
        };

        private readonly List<string> _archiveExtensions = new List<string>
        {
            ".zip", "7z", ".rar"
        };

        public FileService(IOptions<FilesOptions> fileOptions,
            IOptions<UrlsOptions> urlOptions,
            IHostingEnvironment hostingEnvironment,
            IMapper mapper,
            ILogger<FileService> logger)
        {
            _fileOptions = fileOptions.Value;
            _urlOptions = urlOptions.Value;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<string>> UploadImageAsync(IFormFile imageFile)
        {
            string extension = Path.HasExtension(imageFile.FileName)
                ? Path.GetExtension(imageFile.FileName)
                : string.Empty;

            if (!_imageExtensions.Contains(extension))
            {
                return ServiceResult<string>.Fail(EOperationResult.ValidationError, "Invalid extension");
            }

            string fileName = $"image_{Guid.NewGuid()} + {extension}";
            string relativeFilePath = Path.Combine(_fileOptions.UploadFolder,
                _fileOptions.InnerFolders.ImagesFolder, fileName);
            string fullPath = Path.Combine(_hostingEnvironment.ContentRootPath, relativeFilePath);

            if (imageFile.Length > _fileSize)
            {
                CompressImageAndWriteImageToFile(fullPath, imageFile);
            }
            else
            {
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
            }

            string urlPath = Path.Combine(_urlOptions.ServerUrl, relativeFilePath);

            return ServiceResult<string>.Ok(urlPath);
        }

        public async Task<ServiceResult<FileDto>> UploadFileAsync(IFormFile file)
        {
            string extension = Path.HasExtension(file.FileName)
                ? Path.GetExtension(file.FileName)
                : string.Empty;

            var type = GetFileEnumType(extension);

            if (!type.HasValue)
            {
                return ServiceResult<FileDto>.Fail(EOperationResult.ValidationError, "Extension is unsupported");
            }

            string fileType = Enum.GetName(typeof(EFileType), type.Value);
            string fileName = $"{fileType}_{Guid.NewGuid()} + {extension}";
            string relativeFilePath =
                Path.Combine(_fileOptions.UploadFolder, _fileOptions.InnerFolders.FilesFolder, fileType, fileName);

            string fullPath = Path.Combine(_hostingEnvironment.ContentRootPath, relativeFilePath);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            string urlPath = Path.Combine(_urlOptions.ServerUrl, relativeFilePath);

            var newFile = new FileDto
            {
                Name = file.FileName,
                FileType = type.Value,
                Url = urlPath
            };

            return ServiceResult<FileDto>.Ok(newFile);
        }

        private EFileType? GetFileEnumType(string file)
        {
            string extension = Path.HasExtension(file)
                ? Path.GetExtension(file)
                : string.Empty;

            if (_imageExtensions.Contains(extension))
            {
                return EFileType.Image;
            }
            else if (_archiveExtensions.Contains(extension))
            {
                return EFileType.Archive;
            }
            else if (_documentExtensions.Contains(extension))
            {
                return EFileType.Document;
            }
            else if (_mediaExtensions.Contains(extension))
            {
                return EFileType.Audio;
            }

            return null;
        }

        private void CompressImageAndWriteImageToFile(string fullPath, IFormFile imageFile)
        {
            using (var image = new MagickImage(imageFile.OpenReadStream()))
            {
                var size = new MagickGeometry(_pixelSize);
                size.IgnoreAspectRatio = false;
                image.Resize(size);
                image.Write(fullPath);
            }
        }
    }
}