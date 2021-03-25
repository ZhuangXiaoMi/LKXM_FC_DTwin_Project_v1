using LKXM.FCDTwin.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace LKXM.FCDTwin.Repository.PostgreSQL
{
    public class ApiDbContext : DbContext, IDisposable
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        #region DbSet
        public virtual DbSet<TTest> Tests { get; set; }




        #endregion DbSet
    }
}
