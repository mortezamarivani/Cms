using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.ViewComponents
{
    public class StDetailComponent : ViewComponent
    {
        IStRepository _StRepository;
        public StDetailComponent(IStRepository StRepository)
        {
            _StRepository = StRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)
                View("StDetailComponent", _StRepository.GetSt(1)) );
        }

    }

}
