using Walks.API.Models.Domain;

namespace Walks.API.Repositories
{
    public class InMemoryRegionRepository
    {
        
        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>()
            {
                new Region()
                {
                    Id= Guid.NewGuid(),
                    Code = "Some Random Region",
                    Name = "Some Name"


                }
            };
        }

        public Task<Region?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
