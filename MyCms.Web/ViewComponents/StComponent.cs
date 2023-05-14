using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.DomainClasses.St;
using MyCms.Services.Repositories;
using MyCms.ViewModels.St;

namespace MyCms.Web.ViewComponents
{
    public class StComponent : ViewComponent
    {
        IStRepository _StRepository;
        public StComponent(IStRepository StRepository)
        {
            _StRepository = StRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)
                View("StComponent", _StRepository.GetSt(1)) );
        }

    }

}
