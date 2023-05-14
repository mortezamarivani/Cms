using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Config;
using MyCms.DomainClasses.Gallery;
using MyCms.Services.Repositories;

namespace MyCms.Services.Services
{
    public class ConfigRepository : IConfigRepository
    {
        private MyCmsDbContext _db;
        public ConfigRepository(MyCmsDbContext db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public List<Config> GetConfig()
        {
            var ret = _db.Config.ToList();
            return ret;
        }

        public void UpdateConfig(Config Config)
        {
            _db.Entry(Config).State = EntityState.Modified;
        }
    }
}
