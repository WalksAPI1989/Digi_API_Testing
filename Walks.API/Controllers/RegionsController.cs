using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(WalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        //GET All Regions
        // GET: https://localhost:1234/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get region domain models from Database
            //var regionsDomain = await dbContext.Regions.ToListAsync();  // Before using IRepository

            var regionsDomain = await regionRepository.GetAllAsync();

            //Map domain models to DTOs
            //var regionsDto = new List<RegionDto>();    // Before using AutoMapper
            //foreach (var region in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl

            //    });

            //}

            //Map domain models to DTOs
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            //Return DTOs to client
            return Ok(regionsDto);
        }

        //GET Region by ID
        // GET: https://localhost:1234/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            //Get region domain models from Database
            //var regionbyid = dbContext.Regions.Find(id);  // we can use this line to get region using find function
            
            //var regionbyidDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);     // This is before using IRepositroy or Implementing Repository Design pattern

            var regionbyidDomain = await regionRepository.GetByIdAsync(id);   

            if (regionbyidDomain == null)
            {
                return NotFound();
            }

            //Map domain models to DTOs      
            //var regionsDtobyId = new RegionDto    // Before implementing AutoMapper
            //{ 
            //    Id = regionbyidDomain.Id,
            //    Code = regionbyidDomain.Code,
            //    Name = regionbyidDomain.Name,
            //    RegionImageUrl = regionbyidDomain.RegionImageUrl

            //};

            //Map domain models to DTOs
            var regionsDtobyId = mapper.Map<RegionDto>(regionbyidDomain);


            return Ok(regionsDtobyId);
        }

        //Post to create new Regions
        //POST: https://localhost:1234/api/regions/
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            //var regionDomainModel = new Region       // Before Implementing AutoMapper
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
            //};

            //Map or Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            //Use Domain Model to create Region  //Before using IRepository
            //await dbContext.Regions.AddAsync(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map domain model back to DTO
            //var regionDto = new RegionDto            // Before Implementing AutoMapper
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        //Update Region
        //PUT : https://localhost:1234/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check if region exsits
            //var regionbyidDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id); // Before Implementing the IRepository


            //Map DTO to Domain Model
            //var regionbyidDomain = new Region     // Before Implementing AutoMapper
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};

            var regionbyidDomain = mapper.Map<Region>(updateRegionRequestDto);

            //Check if region exsits
            regionbyidDomain = await regionRepository.UpdateAsync(id, regionbyidDomain);

            if(regionbyidDomain == null)
            {
                return NotFound();

            }


            //Map Dto to Domain model  // Before implementing IRepository
            //regionbyidDomain.Code = updateRegionRequestDto.Code;
            //regionbyidDomain.Name = updateRegionRequestDto.Name;
            //regionbyidDomain.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            //Convert Domain model to DTO
            //var regionDto = new RegionDto   // Before Implementing AutoMapper
            //{
            //    Id=regionbyidDomain.Id,
            //    Code=regionbyidDomain.Code,
            //    Name=regionbyidDomain.Name,
            //    RegionImageUrl=regionbyidDomain.RegionImageUrl
            //};

            var regionDto = mapper.Map<RegionDto>(regionbyidDomain);

            return Ok(regionDto);
        }

        // Delete regions
        //DELETE : https://localhost:1234/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //var regionbyidDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);   //before implementing IRepository

            var regionbyidDomain = await regionRepository.DeleteAsync(id);

            if (regionbyidDomain == null)
            {
                return NotFound();

            }

            //Delete region    // Before Implementing IRepository
            //dbContext.Regions.Remove(regionbyidDomain);
            //await dbContext.SaveChangesAsync();

            //retun Deleted region
            //Map Domain model to DTO
            //var regionDto = new RegionDto  // Before Implementing AutoMapper
            //{
            //    Id = regionbyidDomain.Id,
            //    Code = regionbyidDomain.Code,
            //    Name = regionbyidDomain.Name,
            //    RegionImageUrl = regionbyidDomain.RegionImageUrl
            //};

            var regionDto = mapper.Map<RegionDto>(regionbyidDomain);


            return Ok(regionDto);
        }

        //Patch Region
        //Patch : https://localhost:1234/api/regions/{id}
        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] JsonPatchDocument<PatchRegionRequestDto> patchDocument)
        {

            if (patchDocument == null)
            {
                BadRequest();
            }

            var exsitingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (exsitingRegion == null)
            {
                return NotFound();
            }

            //Convert Domain model to DTO
            var patchRegionrequestDto = new PatchRegionRequestDto
            {

                Code = exsitingRegion.Code,
                Name = exsitingRegion.Name,
                RegionImageUrl = exsitingRegion.RegionImageUrl

            };

            patchDocument.ApplyTo(patchRegionrequestDto, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Map Dto to Domain model  // Before implementing IRepository
            exsitingRegion.Code = patchRegionrequestDto.Code;
            exsitingRegion.Name = patchRegionrequestDto.Name;
            exsitingRegion.RegionImageUrl = patchRegionrequestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return Ok(patchRegionrequestDto);
        }

    }
}
