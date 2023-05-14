using System;
using System.Collections.Generic;
using System.Text;
using MyCms.DomainClasses.Skills;
using MyCms.ViewModels.Skills;

namespace MyCms.Services.Repositories
{
   public interface ISkillsRepoitory
   {
       IEnumerable<Skills> GetAllSkills();
        List<ShowSkillsViewModel> GetSkills( int Languge);
        Skills GetSkillsById(int SkillsId);
       void InsertSkills(Skills Skills);
       void UpdateSkills(Skills Skills);
       void DeleteSkills(Skills Skills);
       void DeleteSkills(int SkillsId);
       bool SkillsExists(int SkillsId);
       void Save();


   }
}
