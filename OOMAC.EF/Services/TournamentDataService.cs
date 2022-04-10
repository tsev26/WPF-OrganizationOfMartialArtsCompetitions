using Microsoft.EntityFrameworkCore;
using OOMAC.Domain.Models;
using System;
using System.Collections;
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

        public void StartTournament(int tournamentId, List<Contestant> contestants)
        {
            using (OOMACDBContext context = _contextFactory.CreateDbContext())
            {
                try
                {
                    int contentantsInTournament = contestants.Count;

                    int numberOfGroups = contentantsInTournament / 3;

                    Tournament tournamentContext = new Tournament { Id = tournamentId };

                    tournamentContext.Brackets = new List<Bracket>();

                    for (int group = 0; group < numberOfGroups; group++)
                    {
                        Bracket bracketGroup = new Bracket { TournamentId = tournamentId, Round = 0, Group = group };

                        int contestantPosition = 0 + group;
                        
                        /*
                        Contestant contestantA = new Contestant { Id = contestants[contestantPosition].Id };
                        Contestant contestantB = new Contestant { Id = contestants[contestantPosition+1].Id };
                        Contestant contestantC = new Contestant { Id = contestants[contestantPosition+2].Id };
                        */

                        Match match1 = new Match { Bracket = bracketGroup, ContestantA = contestants[contestantPosition], ContestantB = contestants[contestantPosition + 1] };
                        Match match2 = new Match { Bracket = bracketGroup, ContestantA = contestants[contestantPosition], ContestantB = contestants[contestantPosition + 2] };
                        Match match3 = new Match { Bracket = bracketGroup, ContestantA = contestants[contestantPosition + 1], ContestantB = contestants[contestantPosition + 2] };

                        bracketGroup.Matches = new List<Match>();
                        bracketGroup.Matches.Add(match1);
                        bracketGroup.Matches.Add(match2);
                        bracketGroup.Matches.Add(match3);

                        
                        tournamentContext.Brackets.Add(bracketGroup);

                        
                    }


                    int numberOfRounds = (int)Math.Pow(numberOfGroups, 1.0 / 2);
                    int currentBracket = numberOfRounds;
                    for (int bracketRound = 0; bracketRound <= currentBracket; bracketRound++)
                    {
                        Bracket bracket = new Bracket { TournamentId = tournamentId, Round = bracketRound + 1, Group = 0 };
                        bracket.Matches = new List<Match>();
                        for (int i = 0; i < Math.Pow(2, numberOfRounds); i++)
                        {
                            Match match = new Match { Bracket = bracket };
                            bracket.Matches.Add(match);
                        }


                        tournamentContext.Brackets.Add(bracket);
                        numberOfRounds /= 2;

                    }

                    context.Tournaments.Update(tournamentContext);
                    context.Tournaments.Attach(tournamentContext);
                    context.SaveChanges();
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
