using Learning.API.Data;
using Learning.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learning.API.Repositories
{
    public interface IRegionsRepository
    {
        Task<List<Region>> GetRegionsAsync();
        Task<Region?> GetRegionAsync(Guid id);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region?> UpdateRegionAsync(Guid id, Region region);
        Task<Region?> DeleteRegionAsync(Guid id);
    }

    public class RegionsRepository : IRegionsRepository
    {
        private readonly LearningDbContext dbContext;

        public RegionsRepository(LearningDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetRegionsAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var foundRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (foundRegion == null) 
            {
                return null;
            }

            foundRegion.Code = region.Code;
            foundRegion.Name = region.Name;
            foundRegion.RegionImgUrl = region.RegionImgUrl;

            await dbContext.SaveChangesAsync();
            return foundRegion;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }


            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            return region;
        }
    }
}
