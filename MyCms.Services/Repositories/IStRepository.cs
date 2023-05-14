using MyCms.DomainClasses.Polls;
using MyCms.DomainClasses.St;
using MyCms.ViewModels.St;
using System;
using System.Collections.Generic;

namespace MyCms.Services.Repositories
{
    public interface IStRepository
    {
        ShowStViewModel GetSt(int stCode);
        void InsertSt(St st);
        void UpdateSt(St st);
        void DeleteSt(St st);
        bool StExists(int stID);
        St GetStByIp(int stID);
        void Save();

    }
}
