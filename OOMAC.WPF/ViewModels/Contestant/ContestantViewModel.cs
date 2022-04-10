using OOMAC.Domain.Models;
using OOMAC.WPF.Commands;
using OOMAC.WPF.Services.Navigations;
using OOMAC.WPF.Stores;
using System.Collections.Generic;
using System.Windows.Input;

namespace OOMAC.WPF.ViewModels
{
    public class ContestantViewModel : ViewModelBase
    {
        private ContestantStore _contestantStore;        

        public ContestantViewModel(ContestantStore contestantStore, INavigationService contestantAddOrUpdateNavigationService)
        {
            _contestantStore = contestantStore;
            NavigateContestantAddOrUpdateCommand = new NavigateCommand(contestantAddOrUpdateNavigationService);

            _contestantStore.ContestantStoreChange += ContestantStoreChange;
        }


        public string Loader
        {
            get
            {
                _ = _contestantStore.LoadAsync();

                return "";
            }
        }

        public ICommand NavigateContestantAddOrUpdateCommand { get; }

        public List<Contestant> ContestantList => _contestantStore.Contestants;

        private Contestant _selectedContestant;
        public Contestant SelectedContestant
        {
            get
            {
                return _selectedContestant;
            }
            set
            {
                _selectedContestant = value;
                OnPropertyChanged(nameof(SelectedContestant));
                _contestantStore.SelectedContestant = SelectedContestant;
            }
        }

        private void ContestantStoreChange()
        {
            OnPropertyChanged(nameof(ContestantList));
        }
    }
}
