using Microsoft.EntityFrameworkCore;
using RESTfull.Domain.Model;
using RESTfull.Infrastructure.Repository;

namespace RESTfull.Infrastructure.Tests;

public class UserAttendingsRepositoryTest
{
    private readonly Context _context;

    private const string mainSubject =  "Предметно-ориентированное проектирование " +
                                        "автоматизированных систем управления";
    private List<User> testUsers;

    public UserAttendingsRepository UserAttendingsRepository
    {
        get { return new UserAttendingsRepository(_context); }
    }

    public UserAttendingsRepositoryTest()
    {
        var contextOptions = new DbContextOptionsBuilder<Context>()
            .UseSqlite("Filename=RepositoryTest.db")
            .Options;
        _context = new Context(contextOptions);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        FillDatabase();
    }

    private void FillDatabase()
    {   
        testUsers = new List<User>();
        testUsers.AddRange(new [] {
            new User {
                Id = Guid.NewGuid(),
                FIO = "Лебединский Даниил Алексеевич",
                Email = "lebedinsky@gmail.com",
                Type = UserType.Student,
            },
            new User {
                Id = Guid.NewGuid(),
                FIO = "Апаликов Олег Александрович",
                Email = "apalikov@gmail.com",
                Type = UserType.Student,
            },
            new User {
                Id = Guid.NewGuid(),
                FIO = "Шабанов Александр Павлович",
                Email = "shabanov@gmail.com",
                Type = UserType.Teacher,
            },
        });
        Class[] classes = {
            new Class {
                Id = Guid.NewGuid(),
                Subject = mainSubject,
                TeacherId = testUsers[2].Id,
                Teacher = testUsers[2],
                Group = "ИСТ-912",
                Start = new DateTime (2022, 11, 21, 10, 45, 0),
            },
            new Class {
                Id = Guid.NewGuid(),
                Subject = mainSubject,
                TeacherId = testUsers[2].Id,
                Teacher = testUsers[2],
                Group = "ИСТ-912",
                Start = new DateTime (2022, 12, 5, 10, 45, 0),
            },
            new Class {
                Id = Guid.NewGuid(),
                Subject = "Системный анализ и принятие решений",
                TeacherId = testUsers[2].Id,
                Teacher = testUsers[2],
                Group = "ИСТ-152",
                Start = new DateTime (2022, 12, 6, 13, 00, 0),
            },
            new Class {
                Id = Guid.NewGuid(),
                Subject = mainSubject,
                TeacherId = testUsers[2].Id,
                Teacher = testUsers[2],
                Group = "ИСТ-912",
                Start = new DateTime (2022, 12, 19, 10, 45, 0),
            },
        };
        Attending[] attendings = {
            new Attending {
                Id = Guid.NewGuid(),
                StudyUserId = testUsers[0].Id,
                StudyUser = testUsers[0],
                ClassId = classes[0].Id,
                Class = classes[0],
                Status = AttendingStatus.InvalidReasonSkip,
            },
            new Attending {
                Id = Guid.NewGuid(),
                StudyUserId = testUsers[1].Id,
                StudyUser = testUsers[1],
                ClassId = classes[0].Id,
                Class = classes[0],
                Status = AttendingStatus.ValidReasonSkip,
            },
            new Attending {
                Id = Guid.NewGuid(),
                StudyUserId = testUsers[0].Id,
                StudyUser = testUsers[0],
                ClassId = classes[1].Id,
                Class = classes[1],
                Status = AttendingStatus.Presence,
            },
            new Attending {
                Id = Guid.NewGuid(),
                StudyUserId = testUsers[1].Id,
                StudyUser = testUsers[1],
                ClassId = classes[1].Id,
                Class = classes[1],
                Status = AttendingStatus.Presence,
            },
            new Attending {
                Id = Guid.NewGuid(),
                StudyUserId = testUsers[0].Id,
                StudyUser = testUsers[0],
                ClassId = classes[3].Id,
                Class = classes[3],
                Status = AttendingStatus.Presence,
            },
            new Attending {
                Id = Guid.NewGuid(),
                StudyUserId = testUsers[1].Id,
                StudyUser = testUsers[1],
                ClassId = classes[3].Id,
                Class = classes[3],
                Status = AttendingStatus.InvalidReasonSkip,
            },
            new Attending {
                Id = Guid.NewGuid(),
                StudyUserId = testUsers[2].Id,
                StudyUser = testUsers[2],
                ClassId = classes[0].Id,
                Class = classes[0],
                Status = AttendingStatus.Presence,
            },
            new Attending {
                Id = Guid.NewGuid(),
                StudyUserId = testUsers[2].Id,
                StudyUser = testUsers[2],
                ClassId = classes[1].Id,
                Class = classes[1],
                Status = AttendingStatus.Presence,
            },
            new Attending {
                Id = Guid.NewGuid(),
                StudyUserId = testUsers[2].Id,
                StudyUser = testUsers[2],
                ClassId = classes[2].Id,
                Class = classes[2],
                Status = AttendingStatus.Presence,
            },
            new Attending {
                Id = Guid.NewGuid(),
                StudyUserId = testUsers[2].Id,
                StudyUser = testUsers[2],
                ClassId = classes[3].Id,
                Class = classes[3],
                Status = AttendingStatus.Presence,
            },
        };
        _context.Users.AddRange(testUsers);
        _context.Classes.AddRange(classes);
        _context.Attendings.AddRange(attendings);
        _context.SaveChanges();
    }

    [Fact]
    public async void TestFirstGetMonthAttendings()
    {
        var expected = new List<UserAttending>();
        expected.AddRange(new []
        {
            new UserAttending {
                Subject = mainSubject,
                SubjectStart = new DateTime (2022, 12, 5, 10, 45, 0),
                Status = AttendingStatus.Presence,
            },
            new UserAttending {
                Subject = mainSubject,
                SubjectStart = new DateTime (2022, 12, 19, 10, 45, 0),
                Status = AttendingStatus.Presence,
            },
        });
        var userAttendingsRepository = UserAttendingsRepository;
        var result = await userAttendingsRepository.GetMonthAttendings(
            testUsers[0].Id,
            2022,
            12
        );
        Assert.Equal(expected, result);
    }

    [Fact]
    public async void TestSecondGetMonthAttendings()
    {
        var expected = new List<UserAttending>();
        expected.AddRange(new []
        {
            new UserAttending {
                Subject = mainSubject,
                SubjectStart = new DateTime (2022, 12, 5, 10, 45, 0),
                Status = AttendingStatus.Presence,
            },
            new UserAttending {
                Subject = mainSubject,
                SubjectStart = new DateTime (2022, 12, 19, 10, 45, 0),
                Status = AttendingStatus.InvalidReasonSkip,
            },
        });
        var userAttendingsRepository = UserAttendingsRepository;
        var result = await userAttendingsRepository.GetMonthAttendings(
            testUsers[1].Id,
            2022,
            12
        );
        Assert.Equal(expected, result);
    }

    [Fact]
    public async void TestThirdGetMonthAttendings()
    {
        var expected = new List<UserAttending>();
        expected.AddRange(new []
        {
            new UserAttending {
                Subject = mainSubject,
                SubjectStart = new DateTime (2022, 12, 5, 10, 45, 0),
                Status = AttendingStatus.Presence,
            },
            new UserAttending {
                Subject = "Системный анализ и принятие решений",
                SubjectStart = new DateTime (2022, 12, 6, 13, 00, 0),
                Status = AttendingStatus.Presence,
            },
            new UserAttending {
                Subject = mainSubject,
                SubjectStart = new DateTime (2022, 12, 19, 10, 45, 0),
                Status = AttendingStatus.Presence,
            },
        });
        var userAttendingsRepository = UserAttendingsRepository;
        var result = await userAttendingsRepository.GetMonthAttendings(
            testUsers[2].Id,
            2022,
            12
        );
        Assert.Equal(expected, result);
    }
}
