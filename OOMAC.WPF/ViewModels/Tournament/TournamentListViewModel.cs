using OOMAC.Domain.Models;
using OOMAC.Domain.Models.Calculating;
using OOMAC.EF.Services;
using OOMAC.WPF.Commands;
using OOMAC.WPF.Services.Navigations;
using OOMAC.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OOMAC.WPF.ViewModels
{
    public class TournamentListViewModel : ViewModelBase
    {
        private TournamentStore _tournamentStore;
        
        public TournamentListViewModel(TournamentStore tournamentStore, 
                                      INavigationService tournamentAddOrUpdateNavigationService, 
                                      INavigationService tournamentAddContestantsNavigationService,
                                      INavigationService tournamentSelectedNavigateService,
                                      TournamentDataService tournamentDataService)
        {
            _tournamentStore = tournamentStore;
            NavigateTournamentAddOrUpdateCommand = new NavigateCommand(tournamentAddOrUpdateNavigationService);
            NavigateTournamentAddContestantsCommand = new NavigateCommand(tournamentAddContestantsNavigationService);
            NavigateTournamentSelectedCommand = new NavigateCommand(tournamentSelectedNavigateService);

            StartTournamentCommand = new StartTournamentCommand(this, tournamentDataService);

            _tournamentStore.TournamentStoreChange += TournamentStoreChange;
        }

        public async Task RefreshView(Tournament tournament)
        {
            await _tournamentStore.LoadAsync();
            _tournamentStore.SelectedTournament = tournament;
        }

        public string Loader
        {
            get
            {
                _ = _tournamentStore.LoadAsync();
                _tournamentStore.Unselect();
                SelectedTournament = _tournamentStore.SelectedTournament;

                return "";
            }
        }
        
        public ICommand StartTournamentCommand { get; }
        public ICommand NavigateTournamentAddOrUpdateCommand { get; }
        public ICommand NavigateTournamentAddContestantsCommand { get; }
        public ICommand NavigateTournamentSelectedCommand { get; }

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
                //OnPropertyChanged(nameof(IsTurnamentSelected));
                OnPropertyChanged(nameof(IsTurnamentStarted));
                OnPropertyChanged(nameof(IsTurnamentNotStarted));
                _tournamentStore.SelectedTournament = SelectedTournament;
            }
        }

        public bool IsTurnamentSelected => SelectedTournament != null;

        public bool IsTurnamentStarted => IsTurnamentSelected && (SelectedTournament?.Started ?? false);

        public bool IsTurnamentNotStarted => !IsTurnamentStarted && IsTurnamentSelected;

        private void TournamentStoreChange()
        {
            OnPropertyChanged(nameof(TournamentList));
        }
    }
}
