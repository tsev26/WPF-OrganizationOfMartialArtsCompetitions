using OOMAC.Domain.Models;
using OOMAC.WPF.Commands;
using OOMAC.WPF.Services.Navigations;
using OOMAC.WPF.Stores;
using System.Collections.Generic;
using System.Windows.Input;

namespace OOMAC.WPF.ViewModels
{
    public class TournamentListViewModel : ViewModelBase
    {
        private TournamentStore _tournamentStore;
        
        public TournamentListViewModel(TournamentStore tournamentStore, INavigationService tournamentAddOrUpdateNavigationService, INavigationService tournamentAddContestantsNavigationService)
        {
            _tournamentStore = tournamentStore;
            NavigateTournamentAddOrUpdateCommand = new NavigateCommand(tournamentAddOrUpdateNavigationService);
            NavigateTournamentAddContestantsCommand = new NavigateCommand(tournamentAddContestantsNavigationService);

            _tournamentStore.TournamentStoreChange += TournamentStoreChange;
        }

        public ICommand NavigateTournamentAddOrUpdateCommand { get; }
        public ICommand NavigateTournamentAddContestantsCommand { get; }
        public ICommand NavigateTournamentCommand { get; }

        public List<Tournament> TournamentList => _tournamentStore.Tournaments;

        public Tournament SelectedTournament { get; set; }

        private void TournamentStoreChange()
        {
            OnPropertyChanged(nameof(TournamentList));
        }
    }
}
