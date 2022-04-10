using OOMAC.Domain.Models;
using OOMAC.EF.Services;
using OOMAC.WPF.Services.Navigations;
using OOMAC.WPF.Stores;
using OOMAC.WPF.ViewModels;
using System.Windows.Input;

namespace OOMAC.WPF.Commands
{
    public class CreateNewContestantCommand : CommandBase
    {

        private readonly ContestantAddOrUpdateViewModel _contestantAddOrUpdateViewModel;
        private readonly ContestantDataService _contestantService;
        private readonly ContestantStore _contestantStore;
        public ICommand NavigateContestantCommand { get; }
        public CreateNewContestantCommand(ContestantAddOrUpdateViewModel contestantAddOrUpdateViewModel, 
                                          INavigationService contestantNavigationService,
                                          ContestantDataService contestantService,
                                          ContestantStore contestantStore)
        {
            _contestantAddOrUpdateViewModel = contestantAddOrUpdateViewModel;
            NavigateContestantCommand = new NavigateCommand(contestantNavigationService);
            _contestantService = contestantService;
            _contestantStore = contestantStore;
        }

        public override void Execute(object parameter)
        {
            Contestant newContestant = new Contestant();
            newContestant.FirstName = _contestantAddOrUpdateViewModel.FirstName;
            newContestant.LastName = _contestantAddOrUpdateViewModel.LastName;
            newContestant.Email = _contestantAddOrUpdateViewModel.Email;
            newContestant.DateBorn = _contestantAddOrUpdateViewModel.DateBorn;
            newContestant.TechSkill = _contestantAddOrUpdateViewModel.TechnicalSkill;


            if (_contestantStore.SelectedContestant != null)
            {
                newContestant.Id = _contestantStore.SelectedContestant.Id;
                _ = _contestantService.Update(_contestantStore.SelectedContestant.Id, newContestant);
            }
            else
            {
                _ = _contestantService.Create(newContestant);
            }

            NavigateContestantCommand.Execute(null);
        }
    }
}
