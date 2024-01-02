using Learning.API.Data;
using Learning.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learning.API.Repositories
{
    public interface IWalksRepository
    {
        Task<IEnumerable<Walk>> GetWalksAsync();
        Task<Walk?> GetWalkAsync(Guid id);
        Task<Walk> CreateWalkAsync(Walk Walk);
        Task<Walk?> UpdateWalkAsync(Guid id, Walk Walk);
        Task<Walk?> DeleteWalkAsync(Guid id);
    }

    public class WalksRepository : IWalksRepository
    {
        private readonly LearningDbContext dbContext;

        public WalksRepository(LearningDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Walk>> GetWalksAsync()
        {
            return await dbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .ToListAsync();
        }

        public async Task<Walk?> GetWalkAsync(Guid id)
        {
            return await dbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var foundWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (foundWalk == null)
            {
                return null;
            }

            foundWalk.Name = walk.Name;
            foundWalk.Descripton = walk.Descripton;
            foundWalk.LengthInKm = walk.LengthInKm;
            foundWalk.WalkImgUrl = walk.WalkImgUrl;
            foundWalk.DifficultyId = walk.DifficultyId;
            foundWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return foundWalk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }


            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }
    }
}
