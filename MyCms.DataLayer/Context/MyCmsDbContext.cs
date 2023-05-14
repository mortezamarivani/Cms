using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyCms.DomainClasses.About;
using MyCms.DomainClasses.Config;
using MyCms.DomainClasses.Course;
using MyCms.DomainClasses.Gallery;
using MyCms.DomainClasses.Page;
using MyCms.DomainClasses.PageGroup;
using MyCms.DomainClasses.Polls;
using MyCms.DomainClasses.Project;
using MyCms.DomainClasses.Rank;
using MyCms.DomainClasses.ReciveInfo;
using MyCms.DomainClasses.Simplex;
using MyCms.DomainClasses.Skills;
using MyCms.DomainClasses.St;
using MyCms.DomainClasses.UserX;
using MyCms.DomainClasses.Variable;
using MyCms.ViewModels.Simplex;

namespace MyCms.DataLayer.Context
{
   public class MyCmsDbContext:DbContext
    {
        public MyCmsDbContext(DbContextOptions<MyCmsDbContext> options):base(options)
        {
            
        }

        #region Webseit
        public DbSet<Skills> Skills { get; set; }
        public DbSet<UserX> UserX { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<ReciveInfo> ReciveInfo { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Rank> Rank { get; set; }
        public DbSet<Polls> Polls { get; set; }
        public DbSet<PeriodicElement> PeriodicElement { get; set; }
        #endregion

        #region Config
        public DbSet<Config> Config { get; set; }
        #endregion

        #region Simplex
        public DbSet<Simplex> Simplex { get; set; }
        public DbSet<Variable> Variable { get; set; }
        public DbSet<St> St { get; set; }
        //public DbSet<ShowSimplexViewModel> V_SimplexAndVar { get; set; }

        

        #endregion

    }
}
