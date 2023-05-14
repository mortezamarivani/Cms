using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Gallery;
using MyCms.Services.Repositories;

namespace MyCms.Services.Services
{
    public class GalleryRepository : IGalleryRepository
    {
        private MyCmsDbContext _db;
        public GalleryRepository(MyCmsDbContext db)
        {
            _db = db;
        }
        public bool GalleryExists(int GalleryId)
        {
            return _db.Gallery.Any(c => c.GalleryID == GalleryId);
        }
        public void DeleteGallery(Gallery Gallery)
        {
            _db.Gallery.Remove(Gallery);
        }
        public void DeleteGallery(int GalleryId)
        {
            var Gallery = _db.Gallery.Find(GalleryId);
            DeleteGallery(Gallery);
        }
        public IEnumerable<Gallery> GetAllGallery(int Languge)
        {
                return _db.Gallery.ToList();

        }
        public IEnumerable<Gallery> GetAllGallery(string filetype, int Languge)
        {
            if(filetype == ".pdf")
                return  _db.Gallery.Where(c=> c.SuffixFile == filetype.Trim() && c.Languge == Languge && c.Status == true ).ToList();

            if (Languge == 99)
                return _db.Gallery.ToList();

            return _db.Gallery.Where(c=> c.Languge == Languge && c.SuffixFile != ".pdf" && c.Status == true).ToList();
        }
        public void InsertGallery(Gallery Gallery)
        {
            _db.Gallery.Add(Gallery);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
        public void UpdateGallery(Gallery Gallery)
        {
            _db.Entry(Gallery).State = EntityState.Modified;
        }
        Gallery IGalleryRepository.GetGallery(int GalleryId)
        {
            return _db.Gallery.Find(GalleryId);
        }
        public int GetMaxPicRow()
        {
            return _db.Gallery.Where(c=> c.SuffixFile != ".pdf").Max(c => c.PicRow);
        }
    }
}
