using System;
using System.Collections.Generic;
using System.Text;
using MyCms.DomainClasses.Skills;

namespace MyCms.Services.Repositories
{
   public interface IUserXRepository
    {
      bool UserExists(string UserName , string Password);
   }
}
