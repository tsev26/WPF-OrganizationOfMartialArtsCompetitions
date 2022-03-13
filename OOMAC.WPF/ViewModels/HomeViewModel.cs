using OOMAC.WPF.Commands;
using OOMAC.WPF.Services.Navigations;
using System.Windows.Input;

namespace OOMAC.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ICommand NavigateContestantCommand { get; }

        public HomeViewModel(INavigationService contestantNavigationService)
        {
            NavigateContestantCommand = new NavigateCommand(contestantNavigationService);
        }
    }
}
