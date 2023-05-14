using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.ViewComponents
{
    public class ProjectComponent : ViewComponent
    {
        IProjectRepository _ProjectRepository;
        public ProjectComponent(IProjectRepository ProjectRepository)
        {
            _ProjectRepository = ProjectRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)
                View("ProjectComponent", _ProjectRepository.GetAllProject(@ViewBag.Languge)) );
        }

    }

}
