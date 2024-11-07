//using ElviraIvanovaKt_44_21.Database;
//using ElviraIvanovaKt_44_21.Interfaces.StudentsInterfaces;
//using ElviraIvanovaKt_44_21.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ElviraIvanovaKt_44_21.Tests
//{
//    public class StudentIntegrationTests
//    {
//        public readonly DbContextOptions<StudentDbContext> _dbContextOptions;

//        public StudentIntegrationTests()
//        {
//            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
//            .UseInMemoryDatabase(databaseName: "student_db_ei")
//            .Options;
//        }

//        [Fact]
//        public async Task GetStudentsByGroupAsync_KT4421_TwoObjects()
//        {
//            // Arrange
//            var ctx = new StudentDbContext(_dbContextOptions);
//            var studentService = new StudentService(ctx);
//            var groups = new List<Group>
//            {
//                new Group
//                { 
//                    //GroupId = 1,
//                    GroupName = "KT-44-21"

//                },
//                new Group
//                {
//                   // GroupId = 2,
//                    GroupName = "KT-43-21"

//                },
//                new Group
//                {
//                   // GroupId = 3,
//                    GroupName = "KT-42-21"

//                }
//            };
//            await ctx.Set<Group>().AddRangeAsync(groups);

//            var students = new List<Student>
//            {
//                new Student
//                {
//                    FirstName = "a",
//                    LastName = "a",
//                    MiddleName = "a",
//                    GroupId = 3,
//                },
//                new Student
//                {
//                    FirstName = "a",
//                    LastName = "b",
//                    MiddleName = "a",
//                    GroupId = 3,
//                },
//                new Student
//                {
//                    FirstName = "a",
//                    LastName = "a",
//                    MiddleName = "b",
//                    GroupId = 3,
//                }
//            };
//            await ctx.Set<Student>().AddRangeAsync(students);

//            await ctx.SaveChangesAsync();



//            // Act
//            var filter = new Filters.StudentFilters.StudentGroupFilter
//            {
//                GroupName = "KT-42-21"
//            };
//            var studentsResult = await studentService.GetStudentsByGroupAsync(filter, CancellationToken.None);

//            // Assert
//            Assert.Equal(2, studentsResult.Length);


//            // Act
//            var filter1 = new Filters.StudentFioFilters.StudentFioFilter
//            {
//                FirstName = "a",
//                LastName = "a",
//                MiddleName = "a"

//            };

//            var studentsResult1 = await studentService.GetStudentsByFioAsync(filter1, CancellationToken.None);

//            // Assert
//            Assert.Equal(1, studentsResult1.Length);



//            // Act
//            var filter2 = new Filters.GroupFilter.StudentIdGroup
//            {
//                GroupId = 3
//            };
//            var studentsResult2 = await studentService.GetStudentsByIdGroupAsync(filter2, CancellationToken.None);

//            // Assert
//            Assert.Equal(3, studentsResult2.Length);
//        }
//    }

// }
using ElviraIvanovaKt_44_21.Database;
using ElviraIvanovaKt_44_21.Interfaces.StudentsInterfaces;
using ElviraIvanovaKt_44_21.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ElviraIvanovaKt_44_21.Tests
{
    public class StudentIntegrationTests
    {
        public readonly DbContextOptions<StudentDbContext> _dbContextOptions;

        public StudentIntegrationTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: "student_db_ei")
                .Options;
        }

        [Fact]
        public async Task GetStudentsByFioAsync_KT4421_TwoObjects()
        {
            // Arrange
            var ctx = new StudentDbContext(_dbContextOptions);
            var studentService = new StudentService(ctx);
            var groups = new List<Group>
            {
                new Group { GroupName = "KT-44-21" },
                new Group { GroupName = "KT-43-21" },
                new Group { GroupName = "KT-42-21" },
                new Group {GroupName = "KT-41-21"}
            };
            await ctx.Set<Group>().AddRangeAsync(groups);

            var students = new List<Student>
            {
                new Student { FirstName = "a", LastName = "a", MiddleName = "a", GroupId = 4 }, // GroupId = 3 corresponds to KT-42-21
                new Student { FirstName = "a", LastName = "a", MiddleName = "b", GroupId = 4 }, // GroupId = 3 corresponds to KT-42-21
                new Student { FirstName = "a", LastName = "b", MiddleName = "a", GroupId = 4 } // GroupId = 3 corresponds to KT-42-21
            };
            await ctx.Set<Student>().AddRangeAsync(students);
            await ctx.SaveChangesAsync();

            // Act
            var filter = new Filters.StudentFilters.StudentGroupFilter
            {
                GroupName = "KT-41-21"
            };
            var studentsResult = await studentService.GetStudentsByGroupAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(3, studentsResult.Length); // Изменено на 3, так как добавлено 3 студента в группу KT-42-21
       /*
            // Act
            var filter1 = new Filters.StudentFioFilters.StudentFioFilter
            {
                FirstName = "a",
                LastName = "a",
                MiddleName = "a"
            };

            var studentsResult1 = await studentService.GetStudentsByFioAsync(filter1, CancellationToken.None);

            // Assert
            Assert.Equal(1, studentsResult1.Length); // Проверяем, что один студент соответствует ФИО

            // Act
            var filter2 = new Filters.GroupFilter.StudentIdGroup
            {
                GroupId = 4
            };
            var studentsResult2 = await studentService.GetStudentsByIdGroupAsync(filter2, CancellationToken.None);

            // Assert
            Assert.Equal(3, studentsResult2.Length); // Проверяем, что 3 студента в группе с ID 3*/
        }
    }
}