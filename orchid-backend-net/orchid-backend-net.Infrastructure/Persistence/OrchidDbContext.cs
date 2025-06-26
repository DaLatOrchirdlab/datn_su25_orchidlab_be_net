using Microsoft.EntityFrameworkCore;
using orchid_backend_net.Domain.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Infrastructure.Persistence.Configuration;

namespace orchid_backend_net.Infrastructure.Persistence
{
    public class OrchidDbContext(DbContextOptions<OrchidDbContext> options) : DbContext(options), IUnitOfWork
    {
        public virtual DbSet<Characteristics> Characteristic { get; set; }
        public virtual DbSet<ElementInStage> ElementInStage { get; set; }
        public virtual DbSet<ExperimentLogs> ExperimentLogs { get; set; }
        public virtual DbSet<Hybridizations> Hybridization { get; set; }
        public virtual DbSet<Imgs> Imgs { get; set; }
        public virtual DbSet<InfectedSamples> InfectedSamples { get; set; }
        public virtual DbSet<Linkeds> Linkeds { get; set; }
        public virtual DbSet<Referents> Referents { get; set; }
        public virtual DbSet<ReportAttributes> ReportAttributes { get; set; }
        public virtual DbSet<Stages> Stage { get; set; }
        public virtual DbSet<TasksAssign> TaskAssigns { get; set; }
        public virtual DbSet<TaskAttributes> TaskAttributes { get; set; }
        public virtual DbSet<TissueCultureBatches> TissueCultureBatches { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrchidDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new ConfigCharacteristic());
            modelBuilder.ApplyConfiguration(new ConfigElementStage());
            modelBuilder.ApplyConfiguration(new ConfigUser());
            modelBuilder.ApplyConfiguration(new ConfigTissueCultureBatch());
            modelBuilder.ApplyConfiguration(new ConfigTaskAttribute());
            modelBuilder.ApplyConfiguration(new ConfigTaskAssign());
            modelBuilder.ApplyConfiguration(new ConfigStage());
            modelBuilder.ApplyConfiguration(new ConfigExperimentLog());
            modelBuilder.ApplyConfiguration(new ConfigReferent());
            modelBuilder.ApplyConfiguration(new ConfigReportAttribute());
            modelBuilder.ApplyConfiguration(new ConfigLinked());
            modelBuilder.ApplyConfiguration(new ConfigInfectedSample());
            modelBuilder.ApplyConfiguration(new ConfigImg());
            modelBuilder.ApplyConfiguration(new ConfigHybridization());
        }
        private static void ConfigureModel(ModelBuilder modelBuilder)
        {
            #region User

            #endregion
            #region Seedling

            #endregion
            #region SeedlingAttribute

            #endregion
            #region Characteristic

            #endregion
            #region Task

            #endregion
            #region TaskAssign

            #endregion
            #region Method

            #endregion
            #region Stage

            #endregion
            #region ExperimentLog

            #endregion
            #region LabRoom

            #endregion
            #region TissueCulcureBatch

            #endregion
        }
    }
}
