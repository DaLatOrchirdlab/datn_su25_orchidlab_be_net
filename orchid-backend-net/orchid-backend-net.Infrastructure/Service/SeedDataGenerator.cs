using Microsoft.EntityFrameworkCore;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Service
{
    public static class SeedDataGenerator
    {
        public static async System.Threading.Tasks.Task SeedAsync(DbContext context)
        {
            if (context.Set<ExperimentLog>().Any()) return;

            var labRooms = new List<LabRoom>
        {
            new LabRoom { ID = Guid.NewGuid().ToString(), Name = "Room A", Description = "Sterile lab room A", Status = true },
            new LabRoom { ID = Guid.NewGuid().ToString(), Name = "Room B", Description = "Growth room B", Status = true },
        };
            await context.Set<LabRoom>().AddRangeAsync(labRooms);

            var tissueBatches = new List<TissueCultureBatch>
        {
            new TissueCultureBatch
            {
                ID = Guid.NewGuid().ToString(),
                Name = "Batch A1",
                Description = "First orchid batch",
                LabRoomID = labRooms[0].ID,
                Status = true
            },
            new TissueCultureBatch
            {
                ID = Guid.NewGuid().ToString(),
                Name = "Batch B1",
                Description = "Second orchid batch",
                LabRoomID = labRooms[1].ID,
                Status = true
            }
        };
            await context.Set<TissueCultureBatch>().AddRangeAsync(tissueBatches);

            var methods = new List<Method> { };
            {
                new Method { ID = Guid.NewGuid().ToString(), Name = "Sterilization", Description = "Disinfect material", Type = "Preparation", Status = true };
                new Method { ID = Guid.NewGuid().ToString(), Name = "Subculturing", Description = "Transfer to new media", Type = "Maintenance", Status = true };
            }
            await context.Set<Method>().AddRangeAsync(methods);
            var experimentLogs = new List<ExperimentLog>
        {
            new ExperimentLog
            {
                ID = Guid.NewGuid().ToString(),
                //MethodID = methods[0].ID,
                TissueCultureBatchID = tissueBatches[0].ID,
                Description = "Initial sterilization log",
                Status = 1
            },
            new ExperimentLog
            {
                ID = Guid.NewGuid().ToString(),
                //MethodID = methods[1].ID,
                TissueCultureBatchID = tissueBatches[1].ID,
                Description = "Subculturing log",
                Status = 1
            }
        };
            await context.Set<ExperimentLog>().AddRangeAsync(experimentLogs);

            await context.SaveChangesAsync();
        }
    }
}
