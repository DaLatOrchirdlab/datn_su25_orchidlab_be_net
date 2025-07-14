using Microsoft.EntityFrameworkCore;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Service.SeedData
{
    public static class SeedReports
    {
        public static async Task SeedAsync(DbContext context)
        {
            if (!await context.Set<Reports>().AnyAsync())
            {
                var samples = await context.Set<Samples>().ToListAsync();
                var reports = new List<Reports>
                {
                    new()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Report A",
                        Description = "Tissue culture report for Sample A",
                        TechnicianID = "tech-001",
                        SampleID = samples[0].ID,
                        Status = true
                    },
                    new()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Report B",
                        Description = "Growth stage observation for Sample B",
                        TechnicianID = "tech-002",
                        SampleID = samples[1].ID,
                        Status = true
                    }
                };

                await context.Set<Reports>().AddRangeAsync(reports);
                await context.SaveChangesAsync();
            }
        }
    }
}
