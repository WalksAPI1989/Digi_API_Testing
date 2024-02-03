using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    // /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // Get walk
        //GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            //Get details from Database into Domain Model
            var walkDomainModel = await walkRepository.GetAllAsync();

            //Map domain model to DTO
            var walkDto = mapper.Map<List<WalkDto>>(walkDomainModel);
            return Ok(walkDto);
        }

        //Create walk
        //Post: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map Dto to Domain Model (walk)
           var walkDomainModel =  mapper.Map<Walk>(addWalkRequestDto);
           
            await walkRepository.CreateAsync(walkDomainModel);

            //Map domain model to DTO
            var walkDto= mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }



    }
}
