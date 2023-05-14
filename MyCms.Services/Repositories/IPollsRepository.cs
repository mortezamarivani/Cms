using MyCms.DomainClasses.Polls;
using MyCms.ViewModels.Polls;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCms.Services.Repositories
{
    public interface IPollsRepository
    {
        IEnumerable<Polls> GetAllPolls();
        List<ShowPollsViewModel> GetPolls();
        void InsertPolls(Polls Polls);
        void UpdatePolls(Polls Polls);
        void DeletePolls(Polls Polls);
        bool PollsExists(string Ip);
        Polls GetPollsByIp(string Ip);
        void Save();

    }
}
