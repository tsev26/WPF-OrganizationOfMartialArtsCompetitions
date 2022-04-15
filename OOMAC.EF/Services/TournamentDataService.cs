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


        public Tournament Get(int id)
        {

            using (OOMACDBContext context = _contextFactory.CreateDbContext())
            {
                Tournament entity = context.Set<Tournament>()
                                                            .Include(s => s.Contestans)
                                                            .Include(s => s.Brackets)
                                                            .ThenInclude(s => s.Matches)
                                                            .FirstOrDefault((e) => e.Id == id);
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
                                                                .ThenInclude(s => s.Matches)
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

                        int contestantPosition = 0 + group * 3;


                        Contestant contestantA = new Contestant { Id = contestants[contestantPosition].Id };
                        context.Contestants.Add(contestantA);
                        context.Contestants.Attach(contestantA);

                        Contestant contestantB = new Contestant { Id = contestants[contestantPosition + 1].Id };
                        context.Contestants.Add(contestantB);
                        context.Contestants.Attach(contestantB);

                        Contestant contestantC = new Contestant { Id = contestants[contestantPosition + 2].Id };
                        context.Contestants.Add(contestantC);
                        context.Contestants.Attach(contestantC);

                        Match match1 = new Match { Bracket = bracketGroup, ContestantA = contestantA, ContestantB = contestantB };
                        Match match2 = new Match { Bracket = bracketGroup, ContestantA = contestantC, ContestantB = contestantA };
                        Match match3 = new Match { Bracket = bracketGroup, ContestantA = contestantB, ContestantB = contestantC };

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


        public Tournament SetScore(int matchId, int contestantId, string scoreToAdd)
        {
            using (OOMACDBContext context = _contextFactory.CreateDbContext())
            {
                try
                {
                    //Match match = context.Matches.Single(x => x.Id == matchId);
                    Match match = context.Set<Match>().Include(x => x.ContestantA).Include(x => x.ContestantB).Include(x => x.Bracket).FirstOrDefault(x => x.Id == matchId);
                    //Match match = new Match { Id = matchId };


                    if (match.ContestantA.Id == contestantId)
                    {
                        if (scoreToAdd == "▲" && match.ScoreContestantAString.Contains("▲"))
                        {
                            match.ScoreContestantAString = match.ScoreContestantAString.Replace("▲", string.Empty);
                            match.ScoreContestantBString += "H";
                            match.ScoreContestantB += 1;
                        }
                        else
                        {
                            match.ScoreContestantAString += scoreToAdd;
                            if (scoreToAdd == "●●")
                            {
                                match.ScoreContestantB = 2;
                            }
                            if (scoreToAdd is "M" or "D" or "K" or "T" or "H" or "Ht")
                            {
                                match.ScoreContestantA += 1;
                            }
                        }
                    }
                    else if (match.ContestantB.Id == contestantId)
                    {
                        if (scoreToAdd == "▲" && match.ScoreContestantBString.Contains("▲"))
                        {
                            match.ScoreContestantBString = match.ScoreContestantBString.Replace("▲", string.Empty);
                            match.ScoreContestantAString += "H";
                            match.ScoreContestantA += 1;
                        }
                        else
                        {
                            match.ScoreContestantBString += scoreToAdd;
                            if (scoreToAdd == "●●")
                            {
                                match.ScoreContestantA = 2;
                            }
                            if (scoreToAdd is "M" or "D" or "K" or "T" or "H" or "Ht")
                            {
                                match.ScoreContestantB += 1;
                            }
                        }
                    }

                    context.Matches.Add(match);
                    context.Matches.Attach(match);
                    context.SaveChanges();

                    return Get(match.Bracket.TournamentId);
                }
                catch (Exception e)
                {
                    string x = e.Message;
                }
                return null;
            }
        }

        public async Task<Tournament> Update(int id, Tournament entity)
        {
            return await _genericDataService.Update(id, entity);
        }
    }
}
