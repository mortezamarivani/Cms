using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Polls;
using MyCms.Services.Repositories;
using MyCms.ViewModels.Polls;

namespace MyCms.Services.Services
{
    public class PollsRepository : IPollsRepository
    {
        private MyCmsDbContext _db;
        public PollsRepository(MyCmsDbContext db)
        {
            _db = db;
        }

        List<ShowPollsViewModel> IPollsRepository.GetPolls()
        {
            var results = from p in _db.Polls
                          group p.PollsCode by p.PollsCode into g
                          select new { PollsCode = g.Key, CountPollsCode = g.Count() };

            var countAllPolls = _db.Polls.Select(r => r.PollsID).Count();

            var ret = results.Select(r => new ShowPollsViewModel()
            {
                PollsCode = r.PollsCode,
                CountPollsCode = r.CountPollsCode,
                CountAllPolls = countAllPolls  ,
                Average = (r.CountPollsCode *100) / countAllPolls
            }
          ).ToList();

            return ret;
        }
        public Polls GetPollsByIp(string Ip)
        {
            return _db.Polls.Where(c => c.Ip.Trim() ==Ip.Trim() ).FirstOrDefault();
        }
        public bool PollsExists(string Ip)
        {
            return _db.Polls.Any(c => c.Ip.Trim() == Ip.Trim());
        }

        public void DeletePolls(Polls Polls)
        {
            _db.Polls.Remove(Polls);
        }

        public void DeletePolls(int PollsId)
        {
            var Polls = _db.Polls.Find(PollsId);
            DeletePolls(Polls);
        }



        public IEnumerable<Polls> GetAllPollse()
        {
            return _db.Polls.Where(r=> r.Status == 1 ).ToList();
        }

        public void InsertPolls(Polls Polls)
        {
            _db.Polls.Add(Polls);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void UpdatePolls(Polls Polls)
        {
            _db.Entry(Polls).State = EntityState.Modified;
        }

        public IEnumerable<Polls> GetAllPolls()
        {
            return _db.Polls.Select(r => new Polls()
            {
                CreateDate = r.CreateDate,
                Ip = r.Ip,
                PollsCode = r.PollsCode,
                Status = r.Status
            }
            ).ToList();
        }

     
    }
}
