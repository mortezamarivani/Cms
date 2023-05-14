using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.ViewComponents
{
    public class GalleryComponent: ViewComponent
    {
        IGalleryRepository _galleryRepository;
        public GalleryComponent(IGalleryRepository  galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)
                View("GalleryComponent", _galleryRepository.GetAllGallery("", @ViewBag.Languge)) );
        }

    }
}
