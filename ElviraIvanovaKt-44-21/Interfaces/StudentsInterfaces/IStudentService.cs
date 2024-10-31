using ElviraIvanovaKt_44_21.Models;
using ElviraIvanovaKt_44_21.Database;
using ElviraIvanovaKt_44_21.Filters.StudentFilters;
using Microsoft.EntityFrameworkCore;
using ElviraIvanovaKt_44_21.Filters.StudentFioFilters;
using ElviraIvanovaKt_44_21.Filters.GroupFilter;

namespace ElviraIvanovaKt_44_21.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
        public Task<Student[]> GetStudentsByFioAsync(StudentFioFilter filter, CancellationToken cancellationToken);
        public Task<Student[]> GetStudentsByIdGroupAsync(StudentIdGroup filter, CancellationToken cancellationToken);

    }

    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;
        public StudentService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Student[]> GetStudentsByIdGroupAsync(StudentIdGroup filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>().Where(w => w.Group.GroupId == filter.GroupId).ToArrayAsync(cancellationToken);

            return students;
        }

        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>().Where(w => w.Group.GroupName == filter.GroupName).ToArrayAsync(cancellationToken);

            return students;
        }

        public Task<Student[]> GetStudentsByFioAsync(StudentFioFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>().Where(w => (w.FirstName == filter.FirstName) && (w.MiddleName == filter.MiddleName) && (w.LastName == filter.LastName)).ToArrayAsync(cancellationToken);

            return students;
        }
    }
}