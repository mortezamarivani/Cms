using MyCms.DataLayer.Context;
using MyCms.DataLayer.Migrations;
using MyCms.DomainClasses.Course;
using MyCms.DomainClasses.Rank;
using MyCms.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCms.Services.Services
{
    public class CourseRepository : ICourseRepository
    {
        private MyCmsDbContext _db;
        public CourseRepository(MyCmsDbContext db)
        {
            this._db = db;
        }


        public IEnumerable<Course> GetAllCourse()
        {
            return _db.Course.ToList();
        }

        public IEnumerable<Course> GetAllCourse(int Languge)
        {
            return _db.Course.Where(c => c.Languge == Languge && c.Status == true).ToList();
        }

        public Course GetCourse(int CourseId)
        {
            return _db.Course.Find(CourseId);
        }
    }
}
