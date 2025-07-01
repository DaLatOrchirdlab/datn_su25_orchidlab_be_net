using Microsoft.EntityFrameworkCore;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Service.SeedData
{
    public static class SeedStages
    {
        public static async Task SeedAsync(DbContext context)
        {
            if (!await context.Set<Stages>().AnyAsync())
            {
                var sterilization = await context.Set<Methods>().FirstOrDefaultAsync(m => m.Name == "Sterilization");
                var subculturing = await context.Set<Methods>().FirstOrDefaultAsync(m => m.Name == "Subculturing");

                var stages = new List<Stages>
                {
                    new()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Surface Cleaning",
                        Description = "Initial rinse with water",
                        MethodID = sterilization.ID,
                        DateOfProcessing = 1,
                        Status = true
                    },
                    new()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Disinfecting",
                        Description = "Using alcohol or bleach",
                        MethodID = sterilization.ID,
                        DateOfProcessing = 2,
                        Status = true
                    },
                    new()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Media Preparation",
                        Description = "Prepare fresh media for transfer",
                        MethodID = subculturing.ID,
                        DateOfProcessing = 1,
                        Status = true
                    },
                    new()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Transfer Samples",
                        Description = "Move samples to new containers",
                        MethodID = subculturing.ID,
                        DateOfProcessing = 2,
                        Status = true
                    }
                };

                await context.Set<Stages>().AddRangeAsync(stages);
                await context.SaveChangesAsync();
            }
        }
    }
}
