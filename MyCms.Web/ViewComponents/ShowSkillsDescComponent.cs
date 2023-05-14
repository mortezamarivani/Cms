using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.ViewComponents
{
    public class ShowSkillsDescComponent:ViewComponent
    {
        ISkillsRepoitory _skilsrepository;
        public ShowSkillsDescComponent(ISkillsRepoitory skillsRepoitory)
        {
            _skilsrepository = skillsRepoitory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            //ViewBag.BootstrapClassName = "fa fa-wheelchair";
            return await Task.FromResult((IViewComponentResult)
                View("ShowSkillsDescComponent", _skilsrepository.GetSkills(@ViewBag.Languge)) );
        }
    }
}
