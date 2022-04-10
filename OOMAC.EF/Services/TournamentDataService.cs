using Microsoft.EntityFrameworkCore;
using OOMAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOMAC.EF.Services
{
    public class TournamentDataService
    {
        private readonly OOMACDBContextFactory _contextFactory;
        private readonly GenericDataService<Tournament> _genericDataService;

        public TournamentDataService(OOMACDBContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _genericDataService = new GenericDataService<Tournament>(contextFactory);
        }

        public async Task<Tournament> Create(Tournament entity)
        {
            return await _genericDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _genericDataService.Delete(id);
        }

        public async Task<Tournament> Get(int id)
        {

            using (OOMACDBContext context = _contextFactory.CreateDbContext())
            {
                Tournament entity = await context.Set<Tournament>()
                                                            .Include(s => s.Contestans)
                                                            .Include(s => s.Brackets)
                                                            .Include(s => s.Pools)
                                                            .FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Tournament>> GetAll()
        {
            using (OOMACDBContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Tournament> entities = await context.Set<Tournament>()
                                                                .Include(s => s.Contestans)
                                                                .Include(s => s.Brackets)
                                                                .Include(s => s.Pools)
                                                                .ToListAsync();
                return entities;
            }
        }

        public void AddContestant(int tournamentId,int contestantId)
        {
            using (OOMACDBContext context = _contextFactory.CreateDbContext())
            {
                try
                {
                    Tournament tournament = new Tournament { Id = tournamentId };
                    context.Tournaments.Add(tournament);
                    context.Tournaments.Attach(tournament);

                    Contestant contestant = new Contestant { Id = contestantId };
                    context.Contestants.Add(contestant);
                    context.Contestants.Attach(contestant);

                    tournament.Contestans = new List<Contestant>();
                    tournament.Contestans.Add(contestant);

                    context.SaveChanges();
                } catch (Exception e)
                {
                    string x = e.Message;
                }

            }
        }

        public void RemoveContestant(int tournamentId, int contestantId)
        {
            using (OOMACDBContext context = _contextFactory.CreateDbContext())
            {
                try
                {
                    Contestant contestant = context.Contestants.Include(x => x.Tournaments).Single(x => x.Id == contestantId);

                    context.Contestants.Attach(contestant);

                    Tournament tournamentToDelete = contestant.Tournaments.Find(x => x.Id == tournamentId);
                    if (tournamentToDelete != null)
                    {
                        contestant.Tournaments.Remove(tournamentToDelete);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    string x = e.Message;
                }
            }
        }

        public async Task<Tournament> Update(int id, Tournament entity)
        {
            return await _genericDataService.Update(id, entity);
        }
    }
}
