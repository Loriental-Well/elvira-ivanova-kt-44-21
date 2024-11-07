using ElviraIvanovaKt_44_21.Database;
using ElviraIvanovaKt_44_21.Interfaces.StudentsInterfaces;
using ElviraIvanovaKt_44_21.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElviraIvanovaKt_44_21.Tests
{
    public class FIO
    {
        public readonly DbContextOptions<StudentDbContext> _dbContextOptions;

        public FIO()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: "studentfio_db_ei")
                .Options;
        }

        [Fact]
        public async Task GetStudentsByGroupAsync_KT4421_TwoObjects()
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
            var filter1 = new Filters.StudentFioFilters.StudentFioFilter
            {
                FirstName = "a",
                LastName = "a",
                MiddleName = "a"
            };

            var studentsResult1 = await studentService.GetStudentsByFioAsync(filter1, CancellationToken.None);
            Assert.Equal(1, studentsResult1.Length); // Проверяем, что один студент соответствует ФИО
        }
    }
}