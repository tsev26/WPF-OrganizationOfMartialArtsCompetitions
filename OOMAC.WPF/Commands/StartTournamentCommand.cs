using OOMAC.Domain.Models;
using OOMAC.EF.Services;
using OOMAC.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMAC.WPF.Commands
{
    public class StartTournamentCommand : CommandBase
    {
        private TournamentListViewModel _tournamentListViewModel;
        private TournamentDataService _tournamentDataService;
        public StartTournamentCommand(TournamentListViewModel tournamentListViewModel, TournamentDataService tournamentDataService)
        {
            _tournamentListViewModel = tournamentListViewModel;
            _tournamentDataService = tournamentDataService;
        }
        public override void Execute(object parameter)
        {
            Tournament tournament = _tournamentListViewModel.SelectedTournament;

            List<Contestant> contestants = new List<Contestant>();
            foreach (Contestant contestant in tournament.Contestans)
            {
                Contestant contestantId = new Contestant { Id = contestant.Id };
                contestants.Add(contestantId);
            }

            _tournamentDataService.StartTournament(tournament.Id, contestants);

            _ = _tournamentListViewModel.RefreshView(tournament);
        }
    }
}
