using Microsoft.EntityFrameworkCore;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Service
{
    public static class SeedDataGenerator
    {
        public static async Task SeedAsync(DbContext context)
        {
            if (!context.Set<LabRooms>().Any())
            {
                var labRooms = new List<LabRooms>
                {
                    new LabRooms { ID = Guid.NewGuid().ToString(), Name = "Room A", Description = "Sterile lab room A", Status = true },
                    new LabRooms { ID = Guid.NewGuid().ToString(), Name = "Room B", Description = "Growth room B", Status = true },
                };
                await context.Set<LabRooms>().AddRangeAsync(labRooms);
            }

            if (!context.Set<TissueCultureBatches>().Any())
            {
                var labRooms = context.Set<LabRooms>().ToList();
                var tissueBatches = new List<TissueCultureBatches>
                {
                    new TissueCultureBatches
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Batch A1",
                        Description = "First orchid batch",
                        LabRoomID = labRooms.FirstOrDefault()?.ID,
                        Status = true
                    },
                    new TissueCultureBatches
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Batch B1",
                        Description = "Second orchid batch",
                        LabRoomID = labRooms.Skip(1).FirstOrDefault()?.ID,
                        Status = true
                    }
                };
                await context.Set<TissueCultureBatches>().AddRangeAsync(tissueBatches);
            }

            if (!context.Set<Methods>().Any())
            {
                var methods = new List<Methods>
                {
                    new Methods { ID = Guid.NewGuid().ToString(), Name = "Sterilization", Description = "Disinfect material", Type = "Preparation", Status = true },
                    new Methods { ID = Guid.NewGuid().ToString(), Name = "Subculturing", Description = "Transfer to new media", Type = "Maintenance", Status = true },
                };
                await context.Set<Methods>().AddRangeAsync(methods);
            }

            if (!context.Set<ExperimentLogs>().Any())
            {
                var methods = context.Set<Methods>().ToList();
                var tissueBatches = context.Set<TissueCultureBatches>().ToList();
                var experimentLogs = new List<ExperimentLogs>
                {
                    new ExperimentLogs
                    {
                        ID = Guid.NewGuid().ToString(),
                        MethodID = methods.FirstOrDefault()?.ID,
                        TissueCultureBatchID = tissueBatches.FirstOrDefault()?.ID,
                        Description = "Initial sterilization log",
                        Status = 1
                    },
                    new ExperimentLogs
                    {
                        ID = Guid.NewGuid().ToString(),
                        MethodID = methods.Skip(1).FirstOrDefault()?.ID,
                        TissueCultureBatchID = tissueBatches.Skip(1).FirstOrDefault()?.ID,
                        Description = "Subculturing log",
                        Status = 1
                    }
                };
                await context.Set<ExperimentLogs>().AddRangeAsync(experimentLogs);
            }

            if (!context.Set<Role>().Any())
            {
                var roles = new List<Role>
                {
                    new Role { ID = 1, Name = "Admin", Description = "Administrator with full access", Status = true },
                    new Role { ID = 2, Name = "Researcher", Description = "Research staff with limited access", Status = true },
                    new Role { ID = 3, Name = "Technician", Description = "Lab technician with operational access", Status = true }
                };
                await context.Set<Role>().AddRangeAsync(roles);
            }

            if (!context.Set<Users>().Any())
            {
                var users = new List<Users>
                {
                    new Users
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Admin User",
                        Email = "admin@email.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("admin1234"),
                        PhoneNumber = "1234567890",
                        RoleID = 1,
                        Status = true,
                    },
                    new Users
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Lab Technician",
                        Email = "tech@email.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("tech1234"),
                        PhoneNumber = "213133311221",
                        RoleID = 3,
                        Status = true,
                    },
                    new Users
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Researcher",
                        Email = "researcher@email.com",
                        Password= BCrypt.Net.BCrypt.HashPassword("research1234"),
                        PhoneNumber = "31312213132",
                        RoleID = 2,
                        Status = true,
                    }
                };
                await context.Set<Users>().AddRangeAsync(users);
            }

            await context.SaveChangesAsync();
        }
    }
}
