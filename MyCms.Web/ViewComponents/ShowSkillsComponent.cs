using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.ViewComponents
{
    public class ShowSkillsComponent:ViewComponent
    {
        IAboutRepository _aboutrepository;
        ISkillsRepoitory _skilsrepository;
        public ShowSkillsComponent(ISkillsRepoitory skillsRepoitory , IAboutRepository aboutrepository)
        {
            _skilsrepository = skillsRepoitory;
            _aboutrepository = aboutrepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string Items = string.Empty;
            foreach (var item in _skilsrepository.GetSkills(@ViewBag.Languge))
            {
                if (Items == string.Empty)
                    Items = item.SkillsTitle;
                else
                    Items = Items + "," + item.SkillsTitle;
            }
            

            ViewBag.Items = Items;
            return await Task.FromResult((IViewComponentResult)
                View("ShowSkillsComponent", _aboutrepository.GetAbouts(@ViewBag.Languge)) );
        }

    }
}
