using OOMAC.Domain.Models;
using OOMAC.EF.Services;
using OOMAC.WPF.Stores;
using OOMAC.WPF.ViewModels;
using System;
using System.Windows.Controls;

namespace OOMAC.WPF.Commands
{
    public class SetScoreCommand : CommandBase
    {
        private readonly TournamentDataService _tournamentDataService;
        private readonly TournamentSelectedViewModel _tournamentSelectedViewModel;
        private readonly TournamentStore _tournamentStore;
        public SetScoreCommand(TournamentSelectedViewModel tournamentSelectedViewModel,TournamentDataService tournamentDataService, TournamentStore tournamentStore)
        {
            _tournamentDataService = tournamentDataService;
            _tournamentSelectedViewModel = tournamentSelectedViewModel;
            _tournamentStore = tournamentStore;
        }
        public override void Execute(object parameter)
        {

            
            Match selectedMatch = _tournamentSelectedViewModel.SelectedMatch;
            if (parameter is Button button)
            {
                string setValue = button.Content.ToString();
                if (button.Parent is WrapPanel wrapPanel)
                {
                    if (wrapPanel.Name == "WrapPanelA")
                    {
                        _tournamentStore.SelectedTournament = _tournamentDataService.SetScore(selectedMatch.Id, selectedMatch.ContestantA.Id, setValue);
                    }
                    else if (wrapPanel.Name == "WrapPanelB")
                    {
                        _tournamentStore.SelectedTournament = _tournamentDataService.SetScore(selectedMatch.Id, selectedMatch.ContestantB.Id, setValue);
                    }
                }
            }

        }
    }
}
