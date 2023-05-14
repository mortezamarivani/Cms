using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MyCms.DomainClasses.About;
using MyCms.DomainClasses.Course;
using MyCms.DomainClasses.Gallery;
using MyCms.DomainClasses.Page;
using MyCms.DomainClasses.PageGroup;
using MyCms.DomainClasses.Rank;
using MyCms.DomainClasses.ReciveInfo;
using MyCms.DomainClasses.Skills;
using MyCms.DomainClasses.UserX;
using Project = MyCms.DomainClasses.Project.Project;

namespace MyCms.WebApi
    {
   public class MyCmsDbContext:DbContext
    {
        public MyCmsDbContext(DbContextOptions<MyCmsDbContext> options):base(options)
        {
            
        }

        public DbSet<PageGroup> PageGroups { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Skills> Skills { get; set; }

        public DbSet<UserX> UserX { get; set; }

        public DbSet<About> About { get; set; }

        public DbSet<ReciveInfo> ReciveInfo { get; set; }

        public DbSet<Gallery> Gallery { get; set; }

        public DbSet<Course> Course { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Rank> Rank { get; set; }




    }
}
