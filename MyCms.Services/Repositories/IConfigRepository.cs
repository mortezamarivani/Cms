using System;
using System.Collections.Generic;
using System.Text;
using MyCms.DomainClasses.Config;

namespace MyCms.Services.Repositories
{
   public interface IConfigRepository
    {
        List<Config> GetConfig();
        void UpdateConfig(Config Config);
        void Save();
    }
}
