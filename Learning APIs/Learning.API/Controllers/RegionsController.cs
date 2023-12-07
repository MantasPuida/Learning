using Learning.API.Data;
using Learning.API.Models.Domain;
using Learning.API.Models.DTOs.Region;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetRegions()
        {
            List<Region> regions = dbContext.Regions.ToList();

            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto(region.Id, region.Code, region.Name, region.RegionImgUrl));
            }

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegion([FromRoute] Guid id) 
        {
            //var region = dbContext.Regions.Find(id);

            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            var regionsDto = new RegionDto(region.Id, region.Code, region.Name, region.RegionImgUrl);

            return Ok(regionsDto);
        }

        [HttpPost]
        public IActionResult CreateRegion([FromBody] CreateRegionDto createRegionDto)
        {
            var region = new Region
            {
                RegionImgUrl = createRegionDto.RegionImgUrl,
                Name = createRegionDto.Name,
                Code = createRegionDto.Code
            };

            dbContext.Regions.Add(region);
            dbContext.SaveChanges();

            var regionDto = new RegionDto(region.Id, region.Code, region.Name, region.RegionImgUrl);

            return CreatedAtAction(nameof(GetRegion), new { id = region.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            regionDomainModel.Code = updateRegionDto.Code;
            regionDomainModel.Name = updateRegionDto.Name;
            regionDomainModel.RegionImgUrl = updateRegionDto.RegionImgUrl;

            dbContext.SaveChanges();

            var regionDto = new RegionDto(regionDomainModel.Id, regionDomainModel.Code, regionDomainModel.Name, regionDomainModel.RegionImgUrl);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(regionDomainModel); 
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
