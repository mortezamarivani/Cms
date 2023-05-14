using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.St;
using MyCms.Services.Repositories;
using MyCms.ViewModels.St;

namespace MyCms.Services.Services
{
    public class StRepository : IStRepository
    {
        private MyCmsDbContext _db;
        public StRepository(MyCmsDbContext db)
        {
            _db = db;
        }
    
        ShowStViewModel IStRepository.GetSt(int stCode)
        {
            return _db.St.Where(r => r.StCode == stCode).Select(r=> new ShowStViewModel
            {
                IsZ = r.IsZ,
                StCode=r.StCode,
                StID=r.StID,
                StValue =r.StValue,
                StValueLiner= r.StValueLiner
            }).FirstOrDefault();
        }

        public void InsertSt(St St)
        {
            _db.St.Add(St);
        }
        public void UpdateSt(St St)
        {
            _db.Entry(St).State = EntityState.Modified;
        }

        public void DeleteSt(St St)
        {
            var StRet = _db.St.Where(r => r.StCode == St.StCode).FirstOrDefault();
            if(StRet != null)
                _db.St.Remove(StRet);
        }

        public void DeleteSt(int StCode)
        {
            var St = _db.St.Where(r => r.StCode == StCode).FirstOrDefault();
            DeleteSt(St);
        }
        public void Save()
        {
            _db.SaveChanges();
        }


        public bool StExists(int StID)
        {
            return _db.St.Any(c => c.StID == StID);
        }

        public St GetStByIp(int stID)
        {
            return _db.St.Where(c => c.StID == stID).FirstOrDefault();
        }

    }
}
