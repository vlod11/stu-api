using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using AutoMapper;
using ImageMagick;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.Shared.Options;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.BLL.Services
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

        private readonly List<string> _archieveExtensions = new List<string> 
        {
             ".zip", "7z", ".rar" 
        };

        public FileService(IOptions<FilesOptions> fileOptions,
            IOptions<UrlsOptions> urlOptions,
            IHostingEnvironment hostingEnvironment,
            IMapper mapper)
        {
            _fileOptions = fileOptions.Value;
            _urlOptions = urlOptions.Value;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
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

            string imageName = $"image_{Guid.NewGuid()}";

            string relativeFolderPath = $"{_fileOptions.UploadFolder}/{_fileOptions.InnerFolders.ImagesFolder}";
            string fullPath = Path.Combine(_hostingEnvironment.ContentRootPath, relativeFolderPath);

            string fileName = $"{imageName + extension}";
            fullPath = Path.Combine(fullPath, fileName);

            if (imageFile.Length > _fileSize)
            {
                using (MagickImage image = new MagickImage(imageFile.OpenReadStream()))
                {
                    MagickGeometry size = new MagickGeometry(_pixelSize) { IgnoreAspectRatio = false };
                    image.Resize(size);
                    image.Write(fullPath);
                }
            }
            else
            {
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
            }

            string urlPath = Path.Combine(_urlOptions.AppUrl, relativeFolderPath, fileName);

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
            string fName = $"{fileType}_{Guid.NewGuid()}";
            string relativeFolderPath = $"{_fileOptions.UploadFolder}/{_fileOptions.InnerFolders.FilesFolder}/{fileType}/";
            string fileName = $"{fName + extension}";
            string fullPath = Path.Combine(_hostingEnvironment.ContentRootPath, relativeFolderPath);

            fullPath = Path.Combine(fullPath, fileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            string urlPath = Path.Combine(_urlOptions.AppUrl, relativeFolderPath, fileName);

            FileDto newFile = new FileDto
            {
                Name = file.FileName,
                FileType = (int)type.Value,
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
            else if (_archieveExtensions.Contains(extension))
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
    }
}