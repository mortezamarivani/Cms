using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Page;
using MyCms.DomainClasses.Skills;
using MyCms.Services.Repositories;

namespace MyCms.Services.Services
{
    public class UserXRepoitory : IUserXRepository
    {
        private MyCmsDbContext _db;
        public UserXRepoitory(MyCmsDbContext db)
        {
            _db = db;
        }
        bool IUserXRepository.UserExists(string UserName, string Password)
        {
            var user = _db.UserX.FirstOrDefault(c => c.UserName == UserName && c.Password == Password && c.Status == true);
            if (user == null)
                return false;

            return true;
        }
    }
}
