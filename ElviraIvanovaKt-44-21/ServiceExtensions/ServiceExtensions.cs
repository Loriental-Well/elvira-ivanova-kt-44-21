using ElviraIvanovaKt_44_21.Interfaces.CoursesInterfaces;
using ElviraIvanovaKt_44_21.Interfaces.StudentsInterfaces;

namespace ElviraIvanovaKt_44_21.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICoursesService, CourseService>();

            return services;
        }
    }
}
