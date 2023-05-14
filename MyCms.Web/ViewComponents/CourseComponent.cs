using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.ViewComponents
{
    public class CourseComponent : ViewComponent
    {
        ICourseRepository _CourseRepository;
        public CourseComponent(ICourseRepository CourseRepository)
        {
            _CourseRepository = CourseRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)
                View("CourseComponent", _CourseRepository.GetAllCourse(@ViewBag.Languge)) );
        }

    }

}
