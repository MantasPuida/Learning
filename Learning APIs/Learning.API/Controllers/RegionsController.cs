using Learning.API.Data;
using Learning.API.Models.Domain;
using Learning.API.Models.DTOs.Region;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly LearningDbContext dbContext;

        public RegionsController(LearningDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet] 
        public async Task<IActionResult> GetRegions()
        {
            List<Region> regionsDomain = await dbContext.Regions.ToListAsync();

            var regionsDto = new List<RegionDto>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto(region.Id, region.Code, region.Name, region.RegionImgUrl));
            }

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegion([FromRoute] Guid id) 
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            var regionsDto = new RegionDto(region.Id, region.Code, region.Name, region.RegionImgUrl);

            return Ok(regionsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionDto createRegionDto)
        {
            var region = new Region
            {
                RegionImgUrl = createRegionDto.RegionImgUrl,
                Name = createRegionDto.Name,
                Code = createRegionDto.Code
            };

            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            var regionDto = new RegionDto(region.Id, region.Code, region.Name, region.RegionImgUrl);

            return CreatedAtAction(nameof(GetRegion), new { id = region.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            regionDomainModel.Code = updateRegionDto.Code;
            regionDomainModel.Name = updateRegionDto.Name;
            regionDomainModel.RegionImgUrl = updateRegionDto.RegionImgUrl;

            await dbContext.SaveChangesAsync();

            var regionDto = new RegionDto(regionDomainModel.Id, regionDomainModel.Code, regionDomainModel.Name, regionDomainModel.RegionImgUrl);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(regionDomainModel); 
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
