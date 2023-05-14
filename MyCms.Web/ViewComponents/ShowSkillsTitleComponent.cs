using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.ViewComponents
{
    public class ShowSkillsTitleComponent : ViewComponent
    {
        ISkillsRepoitory _skilsrepository;
        public ShowSkillsTitleComponent(ISkillsRepoitory skillsRepoitory)
        {
            _skilsrepository = skillsRepoitory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            //ViewBag.BootstrapClassName = "fa fa-wheelchair";
            return await Task.FromResult((IViewComponentResult)
                View("ShowSkillsTitleComponent", _skilsrepository.GetSkills(@ViewBag.Languge)) );
        }
    }
}
