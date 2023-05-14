using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.About;
using MyCms.Services.Repositories;
using MyCms.ViewModels.Abouts;

namespace MyCms.Services.Services
{
    public class AboutRepository : IAboutRepository
    {
        private MyCmsDbContext _db;
        public AboutRepository(MyCmsDbContext db)
        {
            _db = db;
        }

        List<ShowAboutViewModel> IAboutRepository.GetAbouts(int Languge)
        {
            return _db.About.Select(r => new ShowAboutViewModel()
            {
                AboutDescription = r.AboutDescription,
                AboutHeader = r.AboutHeader,
                AboutTitle = r.AboutTitle,
                HomeDesc = r.HomeDesc,
                Name = r.Name,
                Languge = r.Languge,
                Status = r.Status
            }
            ).Where(r => r.Languge == Languge && r.Status == true ).ToList();
        }
        public About GetAboutById(int AboutId)
        {
            return _db.About.Where(c => c.AboutID == AboutId ).FirstOrDefault();
        }
        public bool AbouteExists(int AboutId)
        {
            return _db.About.Any(c => c.AboutID == AboutId);
        }

        public void DeleteAboute(About About)
        {
            _db.About.Remove(About);
        }

        public void DeleteAboute(int AboutId)
        {
            var about = _db.About.Find(AboutId);
            DeleteAboute(about);
        }

        public IEnumerable<About> GetAllAboute(int Languge)
        {
            if (Languge == 99 )
                return _db.About.ToList();

            return _db.About.Where(r=> r.Languge == Languge && r.Status == true ).ToList();
        }

        public void InsertAboute(About About)
        {
            _db.About.Add(About);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void UpdateAboute(About About)
        {
            _db.Entry(About).State = EntityState.Modified;
        }
    }
}
