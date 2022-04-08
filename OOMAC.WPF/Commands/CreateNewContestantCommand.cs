using OOMAC.WPF.Services.Navigations;
using OOMAC.WPF.ViewModels;
using System.Windows.Input;

namespace OOMAC.WPF.Commands
{
    public class CreateNewContestantCommand : CommandBase
    {

        //TODO values from contestantAddOrUpdateViewModel use to create new or update in DB
        private readonly ContestantAddOrUpdateViewModel _contestantAddOrUpdateViewModel;
        public ICommand NavigateContestantCommand { get; }
        public CreateNewContestantCommand(ContestantAddOrUpdateViewModel contestantAddOrUpdateViewModel, INavigationService contestantNavigationService)
        {
            _contestantAddOrUpdateViewModel = contestantAddOrUpdateViewModel;
            NavigateContestantCommand = new NavigateCommand(contestantNavigationService);
        }

        public override void Execute(object parameter)
        {
            NavigateContestantCommand.Execute(null);
        }
    }
}
