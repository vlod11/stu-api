using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using UniHub.Common.Constants;
using UniHub.Common.Helpers.Contract;
using UniHub.Data;
using UniHub.Data.Entities;
using UniHub.Data.Interfaces;
using UniHub.Model.Models;
using UniHub.Model.Request;
using UniHub.Services;
using UniHub.UnitTest.TestHelpers;
using Xunit;

namespace UniHub.UnitTest.UnitTests.Services
{
    public class UniversityServiceTest
    {
        private IDateHelper _dateHelperStub;
        private IUniversityRepository _universityRepositoryStub;
        private ICityRepository _cityRepositoryStub;
        private IUnitOfWork _unitOfWorkStub;
        private FakeMapper _mapperMock;
        private UniversityService _universityService;

        public UniversityServiceTest()
        {
            _universityService = CreateUniversityService();
        }

        [Fact]
        public async Task CreateUniversityAsync_AvatarIsNullOrEmpty_ReturnsUniversityWithDefaultAvatar()
        {
            // ARRANGE
            var request = new UniversityAddRequest();

            _cityRepositoryStub.IsExistById(Arg.Any<int>()).Returns(true);
            // ACT
            var result = await _universityService.CreateUniversityAsync(request);
            var university = _mapperMock.Obj as University;
            // ASSERT
            Assert.Equal(DefaultImagesConstants.DefaultImage, university.Avatar);
        }

        [Fact]
        public async Task CreateUniversityAsync_WrongCityId_ReturnsInvokeResultFailed()
        {
            // ARRANGE
            var request = new UniversityAddRequest();

            _cityRepositoryStub.IsExistById(Arg.Any<int>());
            // ACT
            var result = await _universityService.CreateUniversityAsync(request);
            var university = _mapperMock.Obj as University;
            // ASSERT
            Assert.Equal(EOperationResult.EntityNotFound, result.Code);
        }

        [Fact]
        public async Task CreateUniversityAsync_WithCustomAvatar_ReturnsUniversityWithDefaultAvatar()
        {
            // ARRANGE
            var avatar = "Custom avatar";

            var request = new UniversityAddRequest()
            {
                Avatar = avatar
            };

            _cityRepositoryStub.IsExistById(Arg.Any<int>()).Returns(true);
            // ACT
            var result = await _universityService.CreateUniversityAsync(request);
            var university = _mapperMock.Obj as University;
            // ASSERT
            Assert.Equal(avatar, university.Avatar);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetUniversitiesByCityAsync_CorrectCityId_ReturnsUniversities(int cityId)
        {
            // ARRANGE
            var universities = new List<University>
            {
                new University(), new University()
            };

            _universityRepositoryStub.GetUniversitiesByCityAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>())
                .Returns(universities);

            _cityRepositoryStub.IsExistById(Arg.Any<int>()).Returns(true);
            // ACT
            var result = await _universityService.GetListOfUniversitiesAsync(cityId, 1, 1);
            var universitiesResult = _mapperMock.Obj as List<University>;
            // ASSERT
            Assert.Equal(2, universitiesResult.Count);
        }

        [Theory]
        [InlineData(0)]
        public async Task GetListOfUniversitiesAsync_CityIdEqualZero_ReturnInvokeResultFailed(int cityId)
        {
            // ARRANGE
            var universities = new List<University>
            {
                new University(), new University()
            };

            _universityRepositoryStub.GetUniversitiesByCityAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>())
                .Returns(universities);

            _cityRepositoryStub.IsExistById(Arg.Any<int>()).Returns(true);
            // ACT
            var result = await _universityService.GetListOfUniversitiesAsync(cityId, Arg.Any<int>(), Arg.Any<int>());
            var universitiesResult = _mapperMock.Obj as List<University>;
            // ASSERT
            Assert.Equal(EOperationResult.EntityNotFound, result.Code);
        }
        
        private UniversityService CreateUniversityService()
        {
            _dateHelperStub = Substitute.For<IDateHelper>();
            _universityRepositoryStub = Substitute.For<IUniversityRepository>();
            _cityRepositoryStub = Substitute.For<ICityRepository>();
            _unitOfWorkStub = Substitute.For<IUnitOfWork>();
            _mapperMock = new FakeMapper();

            _unitOfWorkStub.UniversityRepository.Returns(_universityRepositoryStub);
            _unitOfWorkStub.CityRepository.Returns(_cityRepositoryStub);

            return new UniversityService(_unitOfWorkStub, _mapperMock, _dateHelperStub);
        }
    }
}