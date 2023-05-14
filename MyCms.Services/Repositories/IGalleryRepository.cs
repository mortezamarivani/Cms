using System;
using System.Collections.Generic;
using System.Text;
using MyCms.DomainClasses.Config;
using MyCms.DomainClasses.Gallery;
using MyCms.DomainClasses.Skills;
using MyCms.ViewModels.Skills;

namespace MyCms.Services.Repositories
{
   public interface IGalleryRepository
    {
 
        IEnumerable<Gallery> GetAllGallery(int Languge);
        IEnumerable<Gallery> GetAllGallery(string filetype, int Languge);
        Gallery GetGallery(int GalleryId);
        void InsertGallery(Gallery Gallery);
        void UpdateGallery(Gallery Gallery);
        void DeleteGallery(Gallery Gallery);
        void DeleteGallery(int GalleryId);
        bool GalleryExists(int GalleryId);
        int GetMaxPicRow();
        void Save();


    }
}
