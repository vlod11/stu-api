using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UniHub.WebApi.Helpers;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.DataAccess
{
    public class SeedDatabase
    {
        private readonly UniHubDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public SeedDatabase(UniHubDbContext dbContext,
            IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
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
                    InitializeEnum<EPostActionType, PostActionType>(_dbContext.PostActionTypes);

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
            if (!_dbContext.Credentials.Any(x => x.Email == "admin@mail.com"))
            {
                var adminUserCredentional = new Credentional()
                {
                    Email = "admin@mail.com",
                    PasswordHash = Authenticate.Hash("qwerty")
                };

                var userProfile = new UsersProfile()
                {
                    RoleId = (int)ERoleType.Admin,
                    Credentional = adminUserCredentional,
                    Username = "Admin",
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

                var dnu = new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                };

                var universities = new List<University> {
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 2",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончар 3",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 4",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 5",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 6",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 7",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 8",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 9",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 10",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 11",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 12",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 13",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 14",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 15",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                },
                new University()
                {
                    FullTitle = "Дніпровський національний університет імені Олеся Гончара 16",
                    ShortTitle = "ДНУ",
                    Description = "Oles Honchar Dnipro National University is an establishments of higher education in Ukraine. It was founded in 1918. The first four faculties were history and linguistics, law, medicine and physics and mathematics.",
                    City = dnipro,
                    Avatar = Constants.DefaultImage
                }
                };

                var fpm = new Faculty()
                {
                    FullTitle = "Факультет Прикладної Математики",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                };

                var faculties = new List<Faculty> {
                new Faculty()
                {
                    FullTitle = "Української й іноземної філології та мистецтвознавства",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Cуспільних наук і міжнародних відносин",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Історичний",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Психології та спеціальної освіти",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Геолого-географічний",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Прикладної математики",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Економіки",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Систем і засобів масової комунікації",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Юридичний",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Фізики, електроніки та комп'ютерних систем",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Фізико-технічний",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Механіко-математичний",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Хімічний",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Біолого-екологічний",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                },
                new Faculty()
                {
                    FullTitle = "Медичних технологій діагностики та реабілітації",
                    ShortTitle = "ФМП",
                    Description = "Це факультет .. Це факультет .. Це факультет ..Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це факультет .. м Це факультет .. Це факультет .. Це факультет .. Це факультет .. Це ф",
                    University = dnu,
                    Avatar = Constants.DefaultImage
                }
                };

                var teacher = new Teacher()
                {
                    FirstName = "Виктория",
                    LastName = "Трактинская",
                    University = dnu
                };

                var subject = new Subject()
                {
                    Title = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                };

                var subjects = new List<Subject> {
                new Subject()
                {
                    Title = "Математичний аналіз 1",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 2",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 3",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 4",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 5",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 6",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 7",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 8",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 9",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 10",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 11",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 12",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 13",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 14",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                },
                new Subject()
                {
                    Title = "Математичний аналіз 15",
                    Description = "",
                    Teacher = teacher,
                    Faculty = fpm,
                    Avatar = Constants.DefaultImage
                }
                };

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

                var post = new Post()
                {
                    Title = "Коллоквиум 1",
                    Description = "FDSFSDSFSDFSDf",
                    Semester = 1,
                    GivenAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    Subject = subject,
                    UserProfile = userProfile,
                    Group = group1,
                    PostLocationTypeId = (int)EPostLocationType.Home,
                    PostValueTypeId = (int)EPostValueType.Solution
                };

                var post1 = new Post()
                {
                    Title = "Коллоквиум 2",
                    Description = "FDSFSDSFSDFSDf",
                    Semester = 2,
                    GivenAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    Subject = subject,
                    UserProfile = userProfile,
                    Group = group1,
                    PostLocationTypeId = (int)EPostLocationType.Home,
                    PostValueTypeId = (int)EPostValueType.Solution
                };

                var answer = new Answer()
                {
                    Description = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA NNNNNNNNNNNNNNNNNNNNNnnn SSSSSSSSSSSSss WWWWWWWW er",
                    Post = post,
                    UserProfile = userProfile
                };

                //save changes
                _dbContext.Credentials.Add(adminUserCredentional);
                _dbContext.UserProfiles.Add(userProfile);
                _dbContext.Countries.Add(ukraine);
                _dbContext.Cities.Add(dnipro);
                _dbContext.Universities.Add(dnu);
                foreach (var university in universities)
                {
                    _dbContext.Universities.Add(university);
                }

                _dbContext.Faculties.Add(fpm);
                foreach (var faculty in faculties)
                {
                    _dbContext.Faculties.Add(faculty);
                }

                _dbContext.Teachers.Add(teacher);
                _dbContext.Subjects.Add(subject);
                foreach (var subject1 in subjects)
                {
                    _dbContext.Subjects.Add(subject1);
                }

                _dbContext.Groups.Add(group1);
                _dbContext.Groups.Add(group2);
                _dbContext.Groups.Add(group3);
                _dbContext.Posts.Add(post);
                _dbContext.Posts.Add(post1);
                _dbContext.Answers.Add(answer);
                _dbContext.SaveChanges();
            }
        }
    }
}