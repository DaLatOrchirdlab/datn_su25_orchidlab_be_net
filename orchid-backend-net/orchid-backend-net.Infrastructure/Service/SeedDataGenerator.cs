using Microsoft.EntityFrameworkCore;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Infrastructure.Service.SeedData;

namespace orchid_backend_net.Infrastructure.Service
{
    public static class SeedDataGenerator
    {
        public static async Task SeedAsync(DbContext context)
        {
            await SeedRoles.SeedAsync(context);
            await SeedUsers.SeedAsync(context);

            await SeedLabRooms.SeedAsync(context);
            await SeedMethods.SeedAsync(context);
            await SeedTissueCultureBatches.SeedAsync(context);
            await SeedExperimentLogs.SeedAsync(context);

            await SeedSeedlings.SeedAsync(context);
            await SeedSeedlingAttributes.SeedAsync(context);
            await SeedCharacteristics.SeedAsync(context);
        }
    }
}
