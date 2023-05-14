using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.ReciveInfo;
using MyCms.Services.Repositories;

namespace MyCms.Services.Services
{
    public class ReciveInfoRepository : IReciveInfoRepository
    {
        private MyCmsDbContext dbContext;

        public ReciveInfoRepository(MyCmsDbContext db)
        {
            dbContext = db;
        }
        public void DeleteReciveInfo(ReciveInfo ReciveInfo)
        {
            dbContext.ReciveInfo.Remove(ReciveInfo);
        }

        public void DeleteReciveInfo(int ReciveInfoId)
        {
            var ReciveInfo = dbContext.ReciveInfo.Find(ReciveInfoId);
            dbContext.ReciveInfo.Remove(ReciveInfo);
        }

        public IEnumerable<ReciveInfo> GetAllReciveInfo()
        {
            return dbContext.ReciveInfo;
        }

        public ReciveInfo GetReciveInfo(int ReciveInfoId)
        {
            return dbContext.ReciveInfo.Find(ReciveInfoId);
        }

        public void InsertReciveInfo(ReciveInfo ReciveInfo)
        {
            dbContext.ReciveInfo.Add(ReciveInfo);
        }

        public bool ReciveInfoExists(int ReciveInfoId)
        {
         return dbContext.ReciveInfo.Any(c => c.ReciveInfoID == ReciveInfoId);

        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
