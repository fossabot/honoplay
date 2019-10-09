using Honoplay.Common.Constants;
using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Honoplay.Persistence
{
    public sealed class HonoplayDbContext : DbContext
    {

        private readonly bool _isTest;

        public HonoplayDbContext(DbContextOptions<HonoplayDbContext> options) : base(options)
        {
            _isTest = true;
        }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantAdminUser> TenantAdminUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<WorkingStatus> WorkingStatuses { get; set; }
        public DbSet<TraineeUser> TraineeUsers { get; set; }
        public DbSet<TrainerUser> TrainerUsers { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<TraineeGroup> TraineeGroups { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<TrainingSeries> TrainingSerieses { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<TrainingCategory> TrainingCategories { get; set; }
        public DbSet<ClassroomTraineeUser> ClassroomTraineeUsers { get; set; }
        public DbSet<ContentFile> ContentFiles { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<TraineeUserAvatar> TraineeUsersAvatars { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HonoplayDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_isTest)
            {
                return;
            }
            optionsBuilder.UseSqlServer(StringConstants.ConnectionString);
        }
    }
}
