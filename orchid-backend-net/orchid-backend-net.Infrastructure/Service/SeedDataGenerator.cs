using Microsoft.EntityFrameworkCore;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Service
{
    public static class SeedDataGenerator
    {
        public static async Task SeedAsync(DbContext context)
        {
            if (context.Set<ExperimentLogs>().Any()) return;

            var labRooms = new List<LabRooms>
        {
            new LabRooms { ID = Guid.NewGuid().ToString(), Name = "Room A", Description = "Sterile lab room A", Status = true },
            new LabRooms { ID = Guid.NewGuid().ToString(), Name = "Room B", Description = "Growth room B", Status = true },
        };
            await context.Set<LabRooms>().AddRangeAsync(labRooms);

            var tissueBatches = new List<TissueCultureBatches>
        {
            new TissueCultureBatches
            {
                ID = Guid.NewGuid().ToString(),
                Name = "Batch A1",
                Description = "First orchid batch",
                LabRoomID = labRooms[0].ID,
                Status = true
            },
            new TissueCultureBatches
            {
                ID = Guid.NewGuid().ToString(),
                Name = "Batch B1",
                Description = "Second orchid batch",
                LabRoomID = labRooms[1].ID,
                Status = true
            }
        };
            await context.Set<TissueCultureBatches>().AddRangeAsync(tissueBatches);

            var methods = new List<Methods>
            {
                new Methods { ID = Guid.NewGuid().ToString(), Name = "Sterilization", Description = "Disinfect material", Type = "Preparation", Status = true },
                new Methods { ID = Guid.NewGuid().ToString(), Name = "Subculturing", Description = "Transfer to new media", Type = "Maintenance", Status = true },
            };
            await context.Set<Methods>().AddRangeAsync(methods);
            var experimentLogs = new List<ExperimentLogs>
        {
            new ExperimentLogs
            {
                ID = Guid.NewGuid().ToString(),
                MethodID = methods[0].ID,
                TissueCultureBatchID = tissueBatches[0].ID,
                Description = "Initial sterilization log",
                Status = 1
            },
            new ExperimentLogs
            {
                ID = Guid.NewGuid().ToString(),
                MethodID = methods[1].ID,
                TissueCultureBatchID = tissueBatches[1].ID,
                Description = "Subculturing log",
                Status = 1
            }
        };
            await context.Set<ExperimentLogs>().AddRangeAsync(experimentLogs);

            await context.SaveChangesAsync();
        }
    }
}
