using MyCms.DomainClasses.Rank;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCms.Services.Repositories
{
    public interface IRankRepository
    {
        IEnumerable<Rank> GetAllRank(int Type);
        IEnumerable<Rank> GetAllRank(int Languge , int Type);
        Rank GetRank(int RankId );

    }
}
