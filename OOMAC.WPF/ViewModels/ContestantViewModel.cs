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

        public ICommand NavigateContestantAddOrUpdateCommand { get; }

        public List<Contestant> ContestantList => _contestantStore.Contestants;

        public Contestant SelectedContestant { get; set; }

        private void ContestantStoreChange()
        {
            OnPropertyChanged(nameof(ContestantList));
        }
    }
}
