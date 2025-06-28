using Microsoft.EntityFrameworkCore;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Service.SeedData
{
    public static class SeedMethods
    {
        public static async Task SeedAsync(DbContext context)
        {
            if (!await context.Set<Methods>().AnyAsync())
            {
                var methods = new List<Methods>
                {
                    new() { ID = Guid.NewGuid().ToString(), Name = "Sterilization", Description = "Disinfect material", Type = "Preparation", Status = true },
                    new() { ID = Guid.NewGuid().ToString(), Name = "Subculturing", Description = "Transfer to new media", Type = "Maintenance", Status = true }
                };

                await context.Set<Methods>().AddRangeAsync(methods);
                await context.SaveChangesAsync();
            }
        }
    }
}
