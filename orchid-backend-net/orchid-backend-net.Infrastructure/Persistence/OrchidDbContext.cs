using Microsoft.EntityFrameworkCore;
using orchid_backend_net.Domain.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Infrastructure.Persistence.Configuration;

namespace orchid_backend_net.Infrastructure.Persistence
{
    public class OrchidDbContext(DbContextOptions<OrchidDbContext> options) : DbContext(options), IUnitOfWork
    {
        public virtual DbSet<Characteristic> Chrateristic { get; set; }
        public virtual DbSet<ElementInStage> ElementInStage { get; set; }
        public virtual DbSet<ExperimentLog> ExperimentLogs { get; set; }
        public virtual DbSet<Hybridization> Hybridization { get; set; }
        public virtual DbSet<Img> Imgs { get; set; }
        public virtual DbSet<InfectedSample> InfectedSamples { get; set; }
        public virtual DbSet<Linked> Linkeds { get; set; }
        public virtual DbSet<Referent> Referents { get; set; }
        public virtual DbSet<ReportAttribute> ReportAttributes { get; set; }
        public virtual DbSet<Stage> Stage { get; set; }
        public virtual DbSet<TaskAssign> TaskAssigns { get; set; }
        public virtual DbSet<TaskAttribute> TaskAttributes { get; set; }
        public virtual DbSet<TissueCultureBatch> TissueCultureBatches { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrchidDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new ConfigUser());
            modelBuilder.ApplyConfiguration(new ConfigTissueCultureBatch());
            modelBuilder.ApplyConfiguration(new ConfigTaskAttribute());
            modelBuilder.ApplyConfiguration(new ConfigTaskAssign());
            modelBuilder.ApplyConfiguration(new ConfigStage());
            modelBuilder.ApplyConfiguration(new ConfigElementStage());
            modelBuilder.ApplyConfiguration(new ConfigExperimentLog());
            modelBuilder.ApplyConfiguration(new ConfigReferent());
            modelBuilder.ApplyConfiguration(new ConfigReportAttribute());
            modelBuilder.ApplyConfiguration(new ConfigLinked());
            modelBuilder.ApplyConfiguration(new ConfigInfectedSample());
            modelBuilder.ApplyConfiguration(new ConfigImg());
            modelBuilder.ApplyConfiguration(new ConfigHybridization());
            modelBuilder.ApplyConfiguration(new ConfigCharacteristic());
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
