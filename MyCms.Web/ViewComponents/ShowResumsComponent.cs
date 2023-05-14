using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.ViewComponents
{
    public class ShowResumsComponent:ViewComponent
    {
        IGalleryRepository _galleryrepository;
        public ShowResumsComponent(IGalleryRepository galleryRepoitory )
        {
            _galleryrepository = galleryRepoitory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)
                View("ShowResumsComponent",_galleryrepository.GetAllGallery(".pdf", @ViewBag.Languge)) );
        }

    }
}
