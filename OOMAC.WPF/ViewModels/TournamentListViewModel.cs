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

        public string Loader
        {
            get
            {
                _tournamentStore.LoadAsync();

                return "";
            }
        }
        

        public ICommand NavigateTournamentAddOrUpdateCommand { get; }
        public ICommand NavigateTournamentAddContestantsCommand { get; }
        public ICommand NavigateTournamentCommand { get; }

        public List<Tournament> TournamentList => _tournamentStore.Tournaments;

        private Tournament _selectedTournament;
        public Tournament SelectedTournament
        {
            get
            {
                return _selectedTournament;
            }
            set
            {
                _selectedTournament = value;
                OnPropertyChanged(nameof(SelectedTournament));
                _tournamentStore.SelectedTournament = SelectedTournament;
            }
        }

        private void TournamentStoreChange()
        {
            OnPropertyChanged(nameof(TournamentList));
        }
    }
}
