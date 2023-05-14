using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.ViewComponents
{
    public class RankComponent : ViewComponent
    {
        IRankRepository _RankRepository;
        public RankComponent(IRankRepository RankRepository)
        {
            _RankRepository = RankRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)
                View("RankComponent", _RankRepository.GetAllRank(@ViewBag.Languge , 1)) );
        }

    }

}
