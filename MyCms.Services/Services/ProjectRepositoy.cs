using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Gallery;
using MyCms.DomainClasses.Project;
using MyCms.Services.Repositories;

namespace MyCms.Services.Services
{
    public class ProjectRepository : IProjectRepository
    {
        private MyCmsDbContext _db;
        public ProjectRepository(MyCmsDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Project> GetAllProject()
        {
           return _db.Project.ToList();
        }
        public IEnumerable<Project> GetAllProject(int Languge)
        {

            return _db.Project.Where(c => c.Languge == Languge && c.Status == true).ToList();
        }

        Project IProjectRepository.GetProject(int ProjectId)
        {
            return _db.Project.Find(ProjectId);
        }
    }
}
