using OOMAC.WPF.Commands;
using OOMAC.WPF.Services.Navigations;
using OOMAC.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OOMAC.WPF.ViewModels
{
    public class TopMenuLayerViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ICommand NavigateContestantCommand { get; }
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateTournamentCommand { get; }

        

        public TopMenuLayerViewModel(NavigationStore navigationStore, INavigationService contestantNavigationService, INavigationService tournamentNavigationService)
        {
            _navigationStore = navigationStore;
            NavigateContestantCommand = new NavigateCommand(contestantNavigationService);
            NavigateTournamentCommand = new NavigateCommand(tournamentNavigationService);

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Title));
        }

        public string Title => _navigationStore.CurrentViewModelName;
    }
}
