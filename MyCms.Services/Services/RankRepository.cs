using MyCms.DataLayer.Context;
using MyCms.DataLayer.Migrations;
using MyCms.DomainClasses.Rank;
using MyCms.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCms.Services.Services
{
    public class RankRepository : IRankRepository
    {
        private MyCmsDbContext _db;
       public RankRepository(MyCmsDbContext db)
        {
            this._db = db;
        }


        public IEnumerable<Rank> GetAllRank(int Type)
        {
            return _db.Rank.Where(r=> r.Type == Type && r.Status==true).ToList();
        }

        public IEnumerable<Rank> GetAllRank(int Languge, int Type)
        {
            return _db.Rank.Where(c => c.Languge == Languge && c.Status == true && c.Type == Type).ToList();
        }

        public Rank GetRank(int RankId)
        {
            return _db.Rank.Find(RankId);
        }
    }
}
