using LOTA.DataAccess.Repository.IRepository;
using LOTA.DataAccess.Repository;
using LOTA.Service.Service.IService;
using LOTA.Service.Service;
using LOTA.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace LOTA.Service
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds services for the LOTA project.
        /// </summary>
        /// <param name="services"> The service collection. </param>
        /// <returns> The updated service collection. </returns>
        public static IServiceCollection AddLOTAProjectServices(this IServiceCollection services)
        {
            // register UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // register Services
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ITutorService, TutorService>();
            services.AddScoped<IQualificationService, QualificationService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITrimesterService, TrimesterService>();
            services.AddScoped<ITrimesterCourseService, TrimesterCourseService>();
            services.AddScoped<IAssessmentService, AssessmentService>();

            return services;
        }
    }
}
