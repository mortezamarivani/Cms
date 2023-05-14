using System;
using System.Collections.Generic;
using System.Text;
using MyCms.DomainClasses.About;
using MyCms.DomainClasses.Skills;
using MyCms.ViewModels.Abouts;
using MyCms.ViewModels.Skills;

namespace MyCms.Services.Repositories
{
   public interface IAboutRepository
    {
       IEnumerable<About> GetAllAboute(int Languge);
        List<ShowAboutViewModel> GetAbouts(int Languge);
        void InsertAboute(About About);
       void UpdateAboute(About About);
       void DeleteAboute(About About);
       void DeleteAboute(int AboutId);
       bool AbouteExists(int AboutId);
        About GetAboutById(int AboutId);
       void Save();
   }
}
