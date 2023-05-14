using MyCms.DomainClasses.Course;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCms.Services.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAllCourse();
        IEnumerable<Course> GetAllCourse(int Languge);
        Course GetCourse(int CourseId);
    }
}
