using System;
using System.Collections.Generic;
using MyCms.DomainClasses.Gallery;
using MyCms.DomainClasses.Project;

namespace MyCms.Services.Repositories
{
   public interface IProjectRepository
    {
 
        IEnumerable<Project> GetAllProject();
        IEnumerable<Project> GetAllProject(int Languge);
        Project GetProject(int ProjectId);


    }
}
