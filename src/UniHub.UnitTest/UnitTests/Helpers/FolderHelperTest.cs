using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using NSubstitute;
using UniHub.Common.Constants;
using UniHub.Common.Enums;
using UniHub.Common.Helpers;
using UniHub.Common.Helpers.Contract;
using UniHub.Common.Options;
using UniHub.Data.Entities;
using Xunit;

namespace UniHub.UnitTest.UnitTests.Helpers
{
    public class FolderHelperTest
    {
        private const string AppDefaultPath = "/Users/ninja/unihub-api-example/src/UniHub.Web/";
        
        private IOptions<FilesOptions> _fileOptionsStub;
        private IHostingEnvironment _hostingEnvironmentStub;
        private IFolderHelper _folderHelper;
        private MockFileSystem _fileSystemStub;

        public FolderHelperTest()
        {
            _folderHelper = GetFolderHelper();
        }
        
        [Fact]
        public async Task CreateFilesFoldersIfNotExist_FoldersNotExist_CreatesFoldersForImages()
        {
            // ARRANGE
            _fileSystemStub.AddDirectory(AppDefaultPath);
            _hostingEnvironmentStub.ContentRootPath.Returns(AppDefaultPath);
            // ACT
            _folderHelper.CreateFilesFoldersIfNotExist();
            // ASSERT
            Assert.True(_fileSystemStub.Directory.Exists(AppDefaultPath + _fileOptionsStub.Value.UploadFolder + "/" + _fileOptionsStub.Value.InnerFolders.ImagesFolder));
        }
        
        [Fact]
        public async Task CreateFilesFoldersIfNotExist_FoldersNotExist_CreatesFoldersForFilesEnum()
        {
            // ARRANGE
            _fileSystemStub.AddDirectory(AppDefaultPath);
            _hostingEnvironmentStub.ContentRootPath.Returns(AppDefaultPath);
            // ACT
            _folderHelper.CreateFilesFoldersIfNotExist();
            // ASSERT
            Assert.True(_fileSystemStub.Directory.Exists(AppDefaultPath + _fileOptionsStub.Value.UploadFolder + "/" + _fileOptionsStub.Value.InnerFolders.FilesFolder + "/" + EFileType.Archive));
        }
        
        [Fact]
        public async Task CreateFilesFoldersIfNotExist_FoldersNotExist_CreatesFoldersForDefaultFiles()
        {
            // ARRANGE
            _fileSystemStub.AddDirectory(AppDefaultPath);
            _hostingEnvironmentStub.ContentRootPath.Returns(AppDefaultPath);
            // ACT
            _folderHelper.CreateFilesFoldersIfNotExist();
            // ASSERT
            Assert.True(_fileSystemStub.Directory.Exists(AppDefaultPath + _fileOptionsStub.Value.UploadFolder + "/" + _fileOptionsStub.Value.InnerFolders.DefaultImagesFolder));
        }

        private IFolderHelper GetFolderHelper()
        {
            _hostingEnvironmentStub = Substitute.For<IHostingEnvironment>();
            _fileOptionsStub = Options.Create(CreateFileOptionsStub());
            _fileSystemStub = new MockFileSystem();

            return new FolderHelper(_hostingEnvironmentStub ,_fileOptionsStub, _fileSystemStub);
        }

        private FilesOptions CreateFileOptionsStub()
        {
            return new FilesOptions()
                   {
                       UploadFolder = "Files",
                       InnerFolders = new InnerFolders()
                                      {
                                          ImagesFolder = "Images",
                                          FilesFolder = "Other",
                                          DefaultImagesFolder = "Default"
                                      }
                   };
        }
    }
}