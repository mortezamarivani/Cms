using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.ViewComponents
{
    public class AboutTitleComponent : ViewComponent
    {
        IAboutRepository _AboutRepository;
        public AboutTitleComponent(IAboutRepository AboutRepository)
        {
            _AboutRepository = AboutRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)
                View("AboutTitleComponent", _AboutRepository.GetAllAboute(@ViewBag.Languge)) );
        }

    }
}
