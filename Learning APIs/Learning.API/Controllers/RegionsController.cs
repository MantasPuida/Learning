using AutoMapper;
using Learning.API.Data;
using Learning.API.Models.Domain;
using Learning.API.Models.DTOs.Region;
using Learning.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionsRepository regionsRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionsRepository regionsRepository, IMapper mapper)
        {
            this.regionsRepository = regionsRepository;
            this.mapper = mapper;
        }

        [HttpGet] 
        public async Task<IActionResult> GetRegions()
        {
            var regionsDomain = await regionsRepository.GetRegionsAsync();
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegion([FromRoute] Guid id) 
        {
            var regionDomain = await regionsRepository.GetRegionAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionsDto = mapper.Map<RegionDto>(regionDomain);
            return Ok(regionsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionDto createRegionDto)
        {
            var regionDomainModel = mapper.Map<Region>(createRegionDto);
            regionDomainModel = await regionsRepository.CreateRegionAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetRegion), new { id = regionDomainModel.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionDto);
            regionDomainModel = await regionsRepository.UpdateRegionAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await regionsRepository.DeleteRegionAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }
    }
}
