using System;
using System.Collections.Generic;
using System.Text;
using MyCms.DomainClasses.ReciveInfo;

namespace MyCms.Services.Repositories
{
   public interface IReciveInfoRepository
    {
       IEnumerable<ReciveInfo> GetAllReciveInfo();
        ReciveInfo GetReciveInfo(int ReciveInfoId);
        void InsertReciveInfo(ReciveInfo ReciveInfo);
       void DeleteReciveInfo(ReciveInfo ReciveInfo);
       void DeleteReciveInfo(int ReciveInfoId);
       bool ReciveInfoExists(int ReciveInfoId);
       void Save();
   }
}
