using OOMAC.Domain.Models;
using OOMAC.EF.Services;
using OOMAC.WPF.Stores;
using OOMAC.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOMAC.WPF.Commands
{
    public class AssignContestantToTournamentCommand : CommandBase
    {
        private readonly TournamentDataService _tournamentDataService;
        private readonly TournamentAddContestantsViewModel _tournamentAddContestantsViewModel;

        public AssignContestantToTournamentCommand(TournamentAddContestantsViewModel tournamentAddContestantsViewModel, TournamentDataService tournamentDataService)
        {
            _tournamentAddContestantsViewModel = tournamentAddContestantsViewModel;
            _tournamentDataService = tournamentDataService;
        }

        public override void Execute(object parameter)
        {
            int tournamentId = _tournamentAddContestantsViewModel.SelectedTournament.Id;
            if (parameter is Button button)
            {
                if (button.Name == "Add" && _tournamentAddContestantsViewModel.SelectedContestant != null)
                {
                    _tournamentDataService.AddContestant(tournamentId, _tournamentAddContestantsViewModel.SelectedContestant.Id);
                }
                else if (button.Name == "Remove" && _tournamentAddContestantsViewModel.SelectedContestantTournament != null)
                {
                    _tournamentDataService.RemoveContestant(tournamentId, _tournamentAddContestantsViewModel.SelectedContestantTournament.Id);
                }

                _ = _tournamentAddContestantsViewModel.updateViewChange();

            }
        }

    }
}
