using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Page;
using MyCms.DomainClasses.Skills;
using MyCms.Services.Repositories;
using MyCms.ViewModels.Skills;

namespace MyCms.Services.Services
{
    public class SkillsRepoitory: ISkillsRepoitory
    {
        private MyCmsDbContext _db;
        public SkillsRepoitory(MyCmsDbContext db)
        {
            _db = db;
        }

        public void DeleteSkills(Skills Skills)
        {
            _db.Entry(Skills).State = EntityState.Deleted;
        }

        public void DeleteSkills(int SkillsId)
        {
            var skill = GetSkillsById(SkillsId);
            DeleteSkills(skill);
        }

        public IEnumerable<Skills> GetAllSkills()
        {
            return _db.Skills.ToList();
        }

        List<ShowSkillsViewModel> ISkillsRepoitory.GetSkills(int Languge)
        {
            return _db.Skills.Select(r => new ShowSkillsViewModel()
            {
                SkillsID=r.SkillsID,
                SkillsTitle=r.SkillsTitle,
                SkillsDescription = r.SkillsDescription,
                BootstrapClassName =r.BootstarpClassName,
                Progress = r.Progress,
                Languge =r.Languge,
                Status = r.Status
            }
            ).Where(r=> r.Languge == Languge && r.Status == true).ToList();
        }

        public Skills GetSkillsById(int SkillsId)
        {
            return _db.Skills.Find(SkillsId);
        }

        public void InsertSkills(Skills Skills)
        {
            _db.Skills.Add(Skills);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public bool SkillsExists(int SkillsId)
        {
            return _db.Skills.Any(c => c.SkillsID == SkillsId);
        }

        public void UpdateSkills(Skills Skills)
        {
            _db.Entry(Skills).State = EntityState.Modified;
        }

    
        //public PageRepoitory(MyCmsDbContext db)
        //{
        //    _db = db;
        //}

        //public IEnumerable<Page> GetAllPage()
        //{
        //    return _db.Pages.ToList();
        //}

        //public Page GetPageById(int pageId)
        //{
        //    return _db.Pages.Find(pageId);
        //}

        //public void InsertPage(Page page)
        //{
        //    _db.Pages.Add(page);
        //}

        //public void UpdatePage(Page page)
        //{
        //    _db.Entry(page).State = EntityState.Modified;
        //}

        //public void DeletePage(Page page)
        //{
        //    _db.Entry(page).State = EntityState.Deleted;
        //}

        //public void DeletePage(int pageId)
        //{
        //    var page = GetPageById(pageId);
        //    DeletePage(page);
        //}

        //public bool PageExists(int pageId)
        //{
        //    return _db.Pages.Any(p => p.PageID == pageId);
        //}

        //public void Save()
        //{
        //    _db.SaveChanges();
        //}
    }
}
