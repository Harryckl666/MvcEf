using System;
using System.Data.Entity;
using Easy.Framework.Core.Domain.Entities;
using Easy.Framework.Data.EF;
using Easy.Framework.Data.EF.Datas;
using Easy.Framework.Data.EF.EntityFramework.DynamicFilters;
using System.Linq;
using ModelToSql.Model;

namespace ModelToSql
{
    public class EasyDbContext : DbContextBase, IDbContext
    {
        public EasyDbContext()
            : base("Easy")
        {
            TraceId = Guid.NewGuid();
        }
        public DbSet<Sys_Company> Companys { get; set; }
        public DbSet<Sys_Depart> SysDeparts { get; set; }
        public DbSet<Sys_Group> sysGroups { get; set; }
        public DbSet<ButtonDic> ButtonDics { get; set; }
        public DbSet<UseButtonDic> UseButtonDics { get; set; }
        public DbSet<Sys_User> Users { get; set; }
        public DbSet<Sys_lcy> lcys { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<City>().ToTable("City");
            //modelBuilder.Entity<StateCity>().ToTable("StateCity");
            //modelBuilder.Entity<WebDictionary>().ToTable("WebDictionary");
            //modelBuilder.Entity<SysTypeDictionary>().ToTable("SysTypeDictionary");
            //modelBuilder.Entity<SuccessfulPublicity>().ToTable("SuccessfulPublicity");
            //  System.Data.Entity.ModelConfiguration.Configuration.TypeConventionConfiguration type= modelBuilder.Types();
            //  modelBuilder.DisFilter<User, bool>(EfFilterNames.SoftDelete, p => p.IsDeleted);

            //modelBuilder.Configurations.Add(new RoleEntityConfiguration());
            //modelBuilder.Configurations.Add(new MemoEntityConfiguration());
        }
    }
}