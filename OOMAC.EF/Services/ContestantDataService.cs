using Microsoft.EntityFrameworkCore;
using OOMAC.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOMAC.EF.Services
{
    public class ContestantDataService
    {
        private readonly OOMACDBContextFactory _contextFactory;
        private readonly GenericDataService<Contestant> _genericDataService;

        public ContestantDataService(OOMACDBContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _genericDataService = new GenericDataService<Contestant>(contextFactory);
        }

        public async Task<Contestant> Create(Contestant entity)
        {
            return await _genericDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _genericDataService.Delete(id);
        }

        public async Task<Contestant> Get(int id)
        {

            using (OOMACDBContext context = _contextFactory.CreateDbContext())
            {
                Contestant entity = await context.Set<Contestant>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Contestant>> GetAll()
        {
            using (OOMACDBContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Contestant> entities = await context.Set<Contestant>()
                                                                .Include(s => s.Tournaments)
                                                                .ToListAsync();
                return entities;
            }
        }

        public async Task<Contestant> Update(int id, Contestant entity)
        {
            return await _genericDataService.Update(id, entity);
        }
    }
}
