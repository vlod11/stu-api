using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UniHub.WebApi.Helpers;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.Common.Options;

namespace UniHub.WebApi.DataAccess
{
    public class SeedDatabase
    {
        private readonly UniHubDbContext _dbContext;
        private readonly IConfiguration _configuration;

        private readonly string _defaultImageUrl;

        public SeedDatabase(UniHubDbContext dbContext,
            IConfiguration configuration,
            IOptions<UrlsOptions> _urlsOptions
            )
        {
            _dbContext = dbContext;
            _configuration = configuration;

            _defaultImageUrl = _urlsOptions.Value.AppUrl + Constants.DefaultImage;
        }

        public void Seed()
        {
            _dbContext.Database.Migrate();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    InitializeEnum<ERoleType, RoleType>(_dbContext.RoleTypes);
                    InitializeEnum<EFileType, FileType>(_dbContext.FileTypes);
                    InitializeEnum<EPostLocationType, PostLocationType>(_dbContext.PostLocationTypes);
                    InitializeEnum<EPostValueType, PostValueType>(_dbContext.PostValueTypes);
                    InitializeEnum<EPostVoteType, PostVoteType>(_dbContext.PostVoteTypes);

                    CreateDefaultInfo();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void InitializeEnum<T1, T2>(DbSet<T2> dbSet)
                                             where T1 : Enum
                                             where T2 : BaseEnum, new()
        {
            IEnumerable<T2> enumDbValues = GetEnumDbValues<T1, T2>();

            IEnumerable<T2> enumDbValuesToAdd = (
                from e in enumDbValues
                join dbe in dbSet on e.Id equals dbe.Id into ae
                from dbe in ae.DefaultIfEmpty()
                where dbe == null
                select e).ToList();

            _dbContext.AddRange(enumDbValuesToAdd);

            _dbContext.SaveChanges();
        }

        private IEnumerable<T2> GetEnumDbValues<T1, T2>()
                                            where T1 : Enum
                                            where T2 : BaseEnum, new()
        {
            foreach (T1 @enum in Enum.GetValues(typeof(T1)))
            {
                int enumValue = (int)(object)@enum;
                yield return new T2()
                {
                    Id = enumValue,
                    Value = Enum.GetName(typeof(T1), enumValue)
                };
            }
        }

        //TODO: delete
        private void CreateDefaultInfo()
        {
            if (!_dbContext.Users.Any(x => x.Email == "admin@mail.com"))
            {
                var user = new User()
                {
                    Email = "admin@mail.com",
                    PasswordHash = Authenticate.Hash("qwerty"),
                    Avatar = _defaultImageUrl,
                    RoleId = (int)ERoleType.Admin,
                    Username = "Admin",
                    IsValidated = true,
                    Description = "AMA ADMIN BITCH!!!"
                };

                var ukraine = new Country()
                {
                    Title = "Ukraine"
                };

                var dnipro = new City()
                {
                    Title = "Dnipro",
                    Country = ukraine
                };

                Teacher teacher = new Teacher();

                var universities = new List<University>();
                var faculties = new List<Faculty>();
                var subjects = new List<Subject>();

                for (int i1 = 0; i1 < 16; i1++)
                {
                    var university = new University()
                    {
                        FullTitle = "Дніпровський національний університет імені Олеся Гончара " + i1,
                        ShortTitle = "ДНУ",
                        Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                        City = dnipro,
                        Avatar = _defaultImageUrl
                    };

                    teacher = new Teacher()
                    {
                        FirstName = "Виктория",
                        LastName = "Трактинская",
                        University = university
                    };

                    universities.Add(university);

                    for (int i2 = 0; i2 < 16; i2++)
                    {
                        var faculty = new Faculty()
                        {
                            FullTitle = "Факультет Прикладної Математики " + i2,
                            ShortTitle = "ФМП",
                            Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                            University = university,
                            Avatar = _defaultImageUrl
                        };

                        faculties.Add(faculty);

                        for (int i3 = 0; i3 < 16; i3++)
                        {
                            var subject = new Subject()
                            {
                                Title = "Математический анализ " + i3,
                                Description = "",
                                Teacher = teacher,
                                Faculty = faculty,
                                Avatar = _defaultImageUrl
                            };

                            subjects.Add(subject);
                        }
                    }
                }

                var group1 = new Group()
                {
                    Id = 1,
                    Title = "ПА",
                    YearStart = 2017,
                    Number = 1
                };

                var group2 = new Group()
                {
                    Id = 2,
                    Title = "ПС",
                    YearStart = 2017,
                    Number = 1
                };

                var group3 = new Group()
                {
                    Id = 3,
                    Title = "ПЗ",
                    YearStart = 2017,
                    Number = 1
                };


                //save changes
                _dbContext.Users.Add(user);
                _dbContext.Countries.Add(ukraine);
                _dbContext.Cities.Add(dnipro);

                _dbContext.Universities.AddRange(universities);

                _dbContext.Faculties.AddRange(faculties);

                _dbContext.Teachers.Add(teacher);
                _dbContext.Subjects.AddRange(subjects);

                _dbContext.Groups.Add(group1);
                _dbContext.Groups.Add(group2);
                _dbContext.Groups.Add(group3);
    
                _dbContext.SaveChanges();
            }
        }
    }
}